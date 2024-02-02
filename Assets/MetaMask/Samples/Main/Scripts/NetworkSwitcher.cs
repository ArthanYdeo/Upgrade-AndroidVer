using System;
using System.Threading.Tasks;
using MetaMask.Unity.Utils;
using Newtonsoft.Json;

namespace MetaMask.Unity.Samples
{
    public class NetworkSwitcher : BindableMonoBehavior
    {
        [Serializable]
        public class NativeCurrency
        {
            [JsonProperty("name")]
            public string Name;

            [JsonProperty("symbol")]
            public string Symbol;

            [JsonProperty("decimals")]
            public int Decimals = 18;
        }

        [Serializable]
        public class ChainIdData
        {
            [JsonProperty("chainId")]
            public string ChainId;
        }
        
        [Serializable]
        public class EthereumChain : ChainIdData
        {
            [JsonProperty("chainName")]
            public string ChainName;
            
            [JsonProperty("nativeCurrency")]
            public NativeCurrency NativeCurrency;
            
            [JsonProperty("rpcUrls")]
            public string[] RpcUrls;
            
            [JsonProperty("blockExplorerUrls")]
            public string[] BlockExplorerUrls;
            
            [JsonProperty("iconUrls")]
            public string[] IconUrls;
        }
        
        [Inject]
        private MetaMaskUnity _metaMask;

        public EthereumChain chainToSwitchTo;

        private void Start()
        {
            var chainId = chainToSwitchTo.ChainId;

            if (!string.IsNullOrWhiteSpace(chainId) && !chainId.StartsWith("0x"))
            {
                chainId = $"0x{int.Parse(chainId):X}";

                chainToSwitchTo.ChainId = chainId;
            }
        }

        public async void AddNetwork()
        {
            await AddNetwork(chainToSwitchTo);
        }

        public async void SwitchNetwork()
        {
            await SwitchToNetwork(chainToSwitchTo);
        }

        public async Task<bool> SwitchToNetwork(EthereumChain chainData)
        {
            chainData ??= chainToSwitchTo;
            
            var chainId = ValidateChainId(chainData);

            try
            {
                var result = await _metaMask.Wallet.Request<object>("wallet_switchEthereumChain", new[] { new ChainIdData() { ChainId = chainId } });

                if (result == null)
                    return true;
            }
            catch (Exception e)
            {
                // If the error code is 4902, the requested chain hasn't been added by MetaMask, and you must request to add it using wallet_addEthereumChain.
                if (e.Message.Contains("wallet_addEthereumChain"))
                {
                    return await AddNetwork(chainData);
                }

                throw;
            }

            // should never happen
            return false;
        }

        public async Task<bool> AddNetwork(EthereumChain chainData)
        {
            chainData ??= chainToSwitchTo;

            ValidateChainId(chainData);
            var result = await _metaMask.Wallet.Request<object>("wallet_addEthereumChain", new[] { chainData });

            if (result == null)
                return true;

            // should never happen, will throw an error if result is not null
            return false;
        }

        private string ValidateChainId(EthereumChain chainData)
        {
            var chainId = chainData.ChainId;
            
            if (!chainId.StartsWith("0x"))
                throw new ArgumentException($"Expected {chainId} to be in hex");

            return chainId;
        }
    }
}