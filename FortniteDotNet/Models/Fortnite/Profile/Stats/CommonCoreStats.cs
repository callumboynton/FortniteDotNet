using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FortniteDotNet.Models.Fortnite.Profile.Stats
{
    public class CommonCoreStats : BaseStats
    {
        [JsonProperty("subscriptions")]
        public object[] Subscriptions { get; set; }

        [JsonProperty("personal_offers")]
        public object PersonalOffers { get; set; }

        [JsonProperty("mtx_purchase_history")]
        public MtxPurchaseHistory MtxPurchaseHistory { get; set; }

        [JsonProperty("import_friends_claimed")]
        public object ImportFriendsClaimed { get; set; }

        [JsonProperty("current_mtx_platform")]
        public string CurrentMtxPlatform { get; set; }

        [JsonProperty("mtx_affiliate")]
        public string MtxAffiliate { get; set; }

        [JsonProperty("daily_purchases")]
        public Purchases DailyPurchases { get; set; }

        [JsonProperty("in_app_purchases")]
        public InAppPurchases InAppPurchases { get; set; }

        [JsonProperty("forced_intro_played")]
        public string ForcedIntroPlayed { get; set; }

        [JsonProperty("undo_timeout")]
        public DateTime UndoTimeout { get; set; }

        [JsonProperty("permissions")]
        public object[] Permissions { get; set; }

        [JsonProperty("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        [JsonProperty("allowed_to_receive_gifts")]
        public bool AllowedToReceiveGifts { get; set; }

        [JsonProperty("gift_history")]
        public GiftHistory GiftHistory { get; set; }

        [JsonProperty("promotion_status")]
        public PromotionStatus PromotionStatus { get; set; }

        [JsonProperty("survey_data")]
        public SurveyData SurveyData { get; set; }

        [JsonProperty("intro_game_played")]
        public bool IntroGamePlayed { get; set; }

        [JsonProperty("ban_status")]
        public BanStatus BanStatus { get; set; }

        [JsonProperty("undo_cooldowns")]
        public List<UndoCooldown> UndoCooldowns { get; set; }

        [JsonProperty("mtx_affiliate_set_time")]
        public DateTime MtxAffiliateSetTime { get; set; }

        [JsonProperty("weekly_purchases")]
        public Purchases WeeklyPurchases { get; set; }

        [JsonProperty("ban_history")]
        public BanHistory BanHistory { get; set; }

        [JsonProperty("monthly_purchases")]
        public Purchases MonthlyPurchases { get; set; }

        [JsonProperty("allowed_to_send_gifts")]
        public bool AllowedToSendGifts { get; set; }

        [JsonProperty("mtx_affiliate_id")]
        public string MtxAffiliateId { get; set; }
    }

    public class MtxPurchaseHistory
    {
        [JsonProperty("refundsUsed")]
        public int RefundsUsed { get; set; }

        [JsonProperty("refundCredits")]
        public int RefundCredits { get; set; }

        [JsonProperty("purchases")]
        public List<MtxPurchase> Purchases { get; set; }
    }

    public class Purchases
    {
        [JsonProperty("lastInterval")]
        public DateTime LastInterval { get; set; }

        [JsonProperty("purchaseList")]
        public Dictionary<string, int> PurchaseList { get; set; }
    }    

    public class InAppPurchases
    {
        [JsonProperty("receipts")]
        public List<string> Receipts { get; set; }

        [JsonProperty("ignoredReceipts")]
        public List<string> IgnoredReceipts { get; set; }

        [JsonProperty("fulfillmentCounts")]
        public Dictionary<string, int> FulfillmentCounts { get; set; }
    }
    
    public class GiftHistory
    {
        [JsonProperty("num_sent")]
        public int NumSent { get; set; }

        [JsonProperty("sentTo")]
        public Dictionary<string, DateTime> SentTo { get; set; }

        [JsonProperty("num_received")]
        public int NumReceived { get; set; }

        [JsonProperty("receivedFrom")]
        public Dictionary<string, DateTime> ReceivedFrom { get; set; }

        [JsonProperty("gifts")]
        public object[] Gifts { get; set; }
    }

    public class PromotionStatus
    {
        [JsonProperty("promoName")]
        public string PromoName { get; set; }

        [JsonProperty("eligible")]
        public bool Eligible { get; set; }

        [JsonProperty("redeemed")]
        public bool Redeemed { get; set; }

        [JsonProperty("notified")]
        public bool Notified { get; set; }
    }

    public class AllSurveysMetadata
    {
        [JsonProperty("numTimesCompleted")]
        public int NumTimesCompleted { get; set; }

        [JsonProperty("lastTimeCompleted")]
        public DateTime LastTimeCompleted { get; set; }
    }

    public class SurveyData
    {
        [JsonProperty("allSurveysMetadata")]
        public object AllSurveysMetadata { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, object> Metadata { get; set; }
    }

    public class BanStatus
    {
        [JsonProperty("bRequiresUserAck")]
        public bool RequiresUserAck { get; set; }

        [JsonProperty("banReasons")]
        public List<string> BanReasons { get; set; }

        [JsonProperty("bBanHasStarted")]
        public bool BanHasStarted { get; set; }

        [JsonProperty("banStartTimeUtc")]
        public DateTime BanStartTime { get; set; }

        [JsonProperty("banDurationDays")]
        public double BanDurationDays { get; set; }

        [JsonProperty("exploitProgramName")]
        public string ExploitProgramName { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonProperty("competitiveBanReason")]
        public string CompetitiveBanReason { get; set; }
    }

    public class UndoCooldown
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }

        [JsonProperty("cooldownExpires")]
        public DateTime CooldownExpires { get; set; }
    }

    public class BanHistory
    {
        [JsonProperty("banCount")]
        public Dictionary<string, int> BanCount { get; set; }

        [JsonProperty("banTier")]
        public object BanTier { get; set; }
    }

    public class MtxPurchase
    {
        [JsonProperty("purchaseId")]
        public string PurchaseId { get; set; }

        [JsonProperty("offerId")]
        public string OfferId { get; set; }

        [JsonProperty("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("freeRefundEligible")]
        public bool FreeRefundEligible { get; set; }

        [JsonProperty("fulfillments")]
        public List<object> Fulfillments { get; set; }

        [JsonProperty("lootResult")]
        public List<LootResult> LootResult { get; set; }

        [JsonProperty("totalMtxPaid")]
        public int TotalMtxPaid { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        [JsonProperty("gameContext")]
        public string GameContext { get; set; }
    }
    
    public class LootResult
    {
        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("itemGuid")]
        public string ItemGuid { get; set; }

        [JsonProperty("itemProfile")]
        public string ItemProfile { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}