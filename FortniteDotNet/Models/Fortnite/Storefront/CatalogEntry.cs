using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using FortniteDotNet.Services;
using System.Collections.Generic;
using FortniteDotNet.Enums.Fortnite;
using FortniteDotNet.Models.Accounts;
using FortniteDotNet.Payloads.Fortnite;
using FortniteDotNet.Models.Fortnite.Mcp;

namespace FortniteDotNet.Models.Fortnite.Storefront
{
    public class CatalogEntry
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }
        
        [JsonProperty("devName")]
        public string DevName { get; set; }

        [JsonProperty("offerType")]
        public string OfferType { get; set; }

        [JsonProperty("fulfillmentIds", NullValueHandling = NullValueHandling.Ignore)]
        public string[] FulfillmentIds { get; set; }

        [JsonProperty("dailyLimit")]
        public int DailyLimit { get; set; }

        [JsonProperty("weeklyLimit")]
        public int WeeklyLimit { get; set; }

        [JsonProperty("monthlyLimit")]
        public int MonthlyLimit { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        [JsonProperty("prices", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryPrice> Prices { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public object Meta { get; set; }

        [JsonProperty("matchFilter", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchFilter { get; set; }

        [JsonProperty("filterWeight", NullValueHandling = NullValueHandling.Ignore)]
        public double FilterWeight { get; set; }

        [JsonProperty("appStoreId")]
        public List<string> AppStoreId { get; set; }

        [JsonProperty("requirements", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryRequirements> Requirements { get; set; }

        [JsonProperty("refundable")]
        public bool Refundable { get; set; }

        [JsonProperty("giftInfo", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryGiftInfo GiftInfo { get; set; }

        [JsonProperty("metaInfo")]
        public List<CatalogEntryMetaInfo> MetaInfo { get; set; }

        [JsonProperty("displayAssetPath")]
        public string DisplayAssetPath { get; set; }

        [JsonProperty("itemGrants", NullValueHandling = NullValueHandling.Ignore)]
        public List<CatalogEntryItemGrant> ItemGrants { get; set; }

        [JsonProperty("catalogGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string CatalogGroup { get; set; }

        [JsonProperty("catalogGroupPriority")]
        public int CatalogGroupPriority { get; set; }

        [JsonProperty("sortPriority")]
        public int SortPriority { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("shortDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortDescription { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("dynamicBundleInfo", NullValueHandling = NullValueHandling.Ignore)]
        public CatalogEntryBundleInfo DynamicBundleInfo { get; set; }

        [JsonProperty("additionalGrants", NullValueHandling = NullValueHandling.Ignore)]
        public string[] AdditionalGrants { get; set; }

        /// <summary>
        /// Gifts this <see cref="CatalogEntry"/> to the provided account IDs from the account bound to the provided <see cref="OAuthSession"/>.
        /// </summary>
        /// <param name="oAuthSession">The <see cref="OAuthSession"/> to use for authentication.</param>
        /// <param name="box">The gift box to use for the gift.</param>
        /// <param name="message">The message to use for the gift.</param>
        /// <param name="quantity">The quantity of the item to gift.</param>
        /// <param name="revision">The operation revision. This parameter is optional.</param>
        /// <param name="accountIds">The account IDs to send the gift to.</param>
        /// <returns>The <see cref="McpResponse"/> of the executed command.</returns>
        [Obsolete("Using this profile command directly may result in gifts being revoked, so proceed with caution!")]
        public async Task<McpResponse> GiftCatalogEntry(OAuthSession oAuthSession, GiftBox box, string message, int quantity = 1, int revision = -1, params string[] accountIds)
            => await FortniteService.GiftCatalogEntry(oAuthSession, new GiftCatalogEntry
            {
                OfferId = OfferId,
                PurchaseQuantity = quantity,
                Currency = Prices[0].CurrencyType,
                CurrencySubType = Prices[0].CurrencySubType,
                ExpectedTotalPrice = Prices[0].FinalPrice,
                ReceiverAccountIds = accountIds,
                GiftWrapTemplateId = box,
                PersonalMessage = message
            }, revision);
    }
}