using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FortniteDotNet.Models.FortniteService.Profile.Stats
{
    public class AthenaStats : BaseStats
    {
        [JsonProperty("favorite_character", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteCharacter { get; set; }

        [JsonProperty("favorite_backpack", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteBackpack { get; set; }

        [JsonProperty("favorite_pickaxe", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoritePickaxe { get; set; }

        [JsonProperty("favorite_glider", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteGlider { get; set; }

        [JsonProperty("favorite_skydivecontrail", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteSkyDiveContrail { get; set; }

        [JsonProperty("favorite_dance", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> FavoriteDance { get; set; }

        [JsonProperty("favorite_itemwraps", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> FavoriteItemWraps { get; set; }

        [JsonProperty("favorite_loadingscreen", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteLoadingScreen { get; set; }

        [JsonProperty("favorite_musicpack", NullValueHandling = NullValueHandling.Ignore)]
        public string FavoriteMusicPack { get; set; }

        [JsonProperty("use_random_loadouts")]
        public bool UseRandomLoadouts { get; set; }
        
        [JsonProperty("past_seasons")]
        public List<Season> PastSeasons { get; set; }
        
        [JsonProperty("season_match_boost")]
        public int SeasonMatchBoost { get; set; }
        
        [JsonProperty("loadouts")]
        public List<string> Loadouts { get; set; }
        
        [JsonProperty("mfa_reward_claimed")]
        public bool MfaRewardClaimed { get; set; }
        
        [JsonProperty("rested_xp_overflow")]
        public int RestedXpOverflow { get; set; }

        [JsonProperty("quest_manager")]
        public QuestManager QuestManager { get; set; }

        [JsonProperty("book_level")]
        public int BookLevel { get; set; }

        [JsonProperty("season_num")]
        public int SeasonNum { get; set; }

        [JsonProperty("season_update")]
        public int SeasonUpdate { get; set; }

        [JsonProperty("book_xp")]
        public int BookXp { get; set; }

        [JsonProperty("permissions")]
        public List<object> Permissions { get; set; }

        [JsonProperty("season")]
        public Season Season { get; set; }

        [JsonProperty("battlestars")]
        public int BattleStars { get; set; }

        [JsonProperty("vote_data")]
        public VoteData VoteData { get; set; }

        [JsonProperty("battlestars_season_total")]
        public int BattleStarsSeasonTotal { get; set; }

        [JsonProperty("book_purchased")]
        public bool BookPurchased { get; set; }

        [JsonProperty("lifetime_wins")]
        public int LifetimeWins { get; set; }

        [JsonProperty("party_assist_quest")]
        public string PartyAssistQuest { get; set; }

        [JsonProperty("purchased_battle_pass_tier_offers")]
        public List<PurchasedBattlePassTierOffer> PurchasedBattlePassTierOffers { get; set; }

        [JsonProperty("rested_xp_exchange")]
        public double RestedXpExchange { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("xp_overflow")]
        public int XpOverflow { get; set; }

        [JsonProperty("rested_xp")]
        public int RestedXp { get; set; }

        [JsonProperty("rested_xp_mult")]
        public double RestedXpMult { get; set; }

        [JsonProperty("season_first_tracking_bits")]
        public List<object> SeasonFirstTrackingBits { get; set; }

        [JsonProperty("accountLevel")]
        public int AccountLevel { get; set; }

        [JsonProperty("competitive_identity")]
        public object CompetitiveIdentity { get; set; }

        [JsonProperty("last_applied_loadout")]
        public string LastAppliedLoadout { get; set; }

        [JsonProperty("daily_rewards")]
        public object DailyRewards { get; set; }

        [JsonProperty("xp")]
        public int Xp { get; set; }

        [JsonProperty("season_friend_match_boost")]
        public int SeasonFriendMatchBoost { get; set; }

        [JsonProperty("last_match_end_datetime")]
        public DateTime LastMatchEndDateTime { get; set; }

        [JsonProperty("active_loadout_index")]
        public int ActiveLoadoutIndex { get; set; }
    }

    public class Season
    {
        [JsonProperty("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonProperty("numWins")]
        public int NumWins { get; set; }

        [JsonProperty("numHighBracket")]
        public int NumHighBracket { get; set; }

        [JsonProperty("numLowBracket")]
        public int NumLowBracket { get; set; }

        [JsonProperty("seasonXp")]
        public int SeasonXp { get; set; }

        [JsonProperty("seasonLevel")]
        public int SeasonLevel { get; set; }

        [JsonProperty("bookXp")]
        public int BookXp { get; set; }

        [JsonProperty("bookLevel")]
        public int BookLevel { get; set; }

        [JsonProperty("purchasedVIP")]
        public bool PurchasedVIP { get; set; }
    }
    
    public class PurchasedBattlePassTierOffer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
    
    public class QuestManager
    {
        [JsonProperty("dailyLoginInterval")]
        public DateTime DailyLoginInterval { get; set; }

        [JsonProperty("dailyQuestRerolls")]
        public int DailyQuestRerolls { get; set; }
    }
    
    public class VoteData
    {
        [JsonProperty("electionId")]
        public string ElectionId { get; set; }

        [JsonProperty("voteHistory")]
        public Dictionary<string, Vote> VoteHistory { get; set; }

        [JsonProperty("votesRemaining")]
        public int VotesRemaining { get; set; }

        [JsonProperty("lastVoteGranted")]
        public DateTime LastVoteGranted { get; set; }
    }

    public class Vote
    {
        [JsonProperty("voteCount")]
        public int VoteCount { get; set; }

        [JsonProperty("firstVoteAt")]
        public DateTime FirstVoteAt { get; set; }

        [JsonProperty("lastVoteAt")]
        public DateTime LastVoteAt { get; set; }
    }
}