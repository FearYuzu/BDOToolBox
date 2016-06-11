using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BlackDesert.SharedLibs
{
    public class newLanguageData
    {
        private class columnKeys
        {
            public string Name;

            public int index;

            public columnKeys(string n)
            {
                this.Name = n;
                this.index = -1;
            }
        }

        private class Row
        {
            public Row(BinaryReader br, Dictionary<string, newLanguageData.columnKeys> position, DataTable grid, Dictionary<string, string> lookup, string templateName)
            {
                br.BaseStream.Position += 8L;
                int num = br.ReadInt32();
                Dictionary<int, char> dictionary = new Dictionary<int, char>();
                if (position != null)
                {
                    foreach (KeyValuePair<string, newLanguageData.columnKeys> current in position)
                    {
                        dictionary.Add(current.Value.index, current.Value.Name.ToArray<char>()[0]);
                    }
                }
                int num2 = 0;
                object[] array = null;
                if (position != null)
                {
                    array = new object[position.Count];
                }
                for (int i = 0; i < num; i++)
                {
                    string text = Encoding.Unicode.GetString(br.ReadBytes((int)(2L * br.ReadInt64()))).Replace("\n", "\\n");
                    if (text.Contains("갑옷, 신발 세트 효과 : <PAColor0xffe9bd23>최대 생명력 +150<PAOldColor>"))
                    {
                        text = text;
                    }
                    if (position != null && dictionary.ContainsKey(i))
                    {
                        if (lookup != null && lookup.ContainsKey(text))
                        {
                            text = lookup[text];
                        }
                        text = text.Replace("\\n", "\n");
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = "<null>";
                        }
                        array[num2] = text;
                        num2++;
                    }
                }
                if (array != null)
                {
                    grid.Rows.Add(array);
                }
            }
        }

        private class TemplateHeader
        {
            public string templateName;

            public string[] columnsExport;

            public int rowCount;

            public DataTable dt = new DataTable();

            public TemplateHeader(BinaryReader br, Dictionary<string, string> lookup)
            {
                this.templateName = Encoding.Unicode.GetString(br.ReadBytes((int)(2L * br.ReadInt64())));
                br.BaseStream.Position += 8L;
                int num = br.ReadInt32();
                int num2 = 0;
                if (newLanguageData.colPos.ContainsKey(this.templateName))
                {
                    this.columnsExport = new string[newLanguageData.colPos[this.templateName].Count<KeyValuePair<string, newLanguageData.columnKeys>>()];
                }
                for (int i = 0; i < num; i++)
                {
                    string @string = Encoding.Unicode.GetString(br.ReadBytes((int)(2L * br.ReadInt64())));
                    if (newLanguageData.colPos.ContainsKey(this.templateName) && newLanguageData.colPos[this.templateName].ContainsKey(@string))
                    {
                        newLanguageData.colPos[this.templateName][@string].index = i;
                        this.columnsExport[num2] = newLanguageData.colPos[this.templateName][@string].Name;
                        num2++;
                    }
                }
                this.rowCount = br.ReadInt32();
                if (this.columnsExport != null && this.columnsExport.Count<string>() > 0)
                {
                    for (int j = 0; j < this.columnsExport.Count<string>(); j++)
                    {
                        this.dt.Columns.Add();
                    }
                }
                for (int k = 0; k < this.rowCount; k++)
                {
                    new newLanguageData.Row(br, newLanguageData.colPos.ContainsKey(this.templateName) ? newLanguageData.colPos[this.templateName] : null, this.dt, lookup, this.templateName);
                }
                int num3 = br.ReadInt32();
                for (int l = 0; l < num3; l++)
                {
                    br.ReadBytes((int)(2L * br.ReadInt64() + 4L));
                }
                num3 = br.ReadInt32();
                for (int m = 0; m < num3; m++)
                {
                    br.ReadBytes((int)(2L * br.ReadInt64() + 4L));
                }
            }
        }

        private List<newLanguageData.TemplateHeader> templateHeaders;

        private static Dictionary<string, Dictionary<string, newLanguageData.columnKeys>> colPos;

        public newLanguageData()
        {
            newLanguageData.colPos = new Dictionary<string, Dictionary<string, newLanguageData.columnKeys>>();
            Dictionary<string, newLanguageData.columnKeys> dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Enchant", new newLanguageData.columnKeys("^Enchant"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("1_LongSword", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Enchant", new newLanguageData.columnKeys("^Enchant"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("2_Blunt", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Enchant", new newLanguageData.columnKeys("^Enchant"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("3_TwoHandSword", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Enchant", new newLanguageData.columnKeys("^Enchant"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("4_Bow", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Enchant", new newLanguageData.columnKeys("^Enchant"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("5_Dagger", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("AuctionNo", new newLanguageData.columnKeys("^AuctionNo"));
            dictionary.Add("AuctionDesc", new newLanguageData.columnKeys("~AuctionDesc"));
            newLanguageData.colPos.Add("Auction_InfoTable", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("AbilityName", new newLanguageData.columnKeys("~AbilityName"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("Awakening_Ability_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("ConditionDesc", new newLanguageData.columnKeys("~ConditionDesc"));
            newLanguageData.colPos.Add("BuffCondition_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("BuffName", new newLanguageData.columnKeys("~BuffName"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("Buff_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Key", new newLanguageData.columnKeys("^Key"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            dictionary.Add("Keyword", new newLanguageData.columnKeys("~Keyword"));
            newLanguageData.colPos.Add("Card_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ProductNo", new newLanguageData.columnKeys("^ProductNo"));
            dictionary.Add("ProductName", new newLanguageData.columnKeys("~ProductName"));
            dictionary.Add("DisplayName", new newLanguageData.columnKeys("~DisplayName"));
            dictionary.Add("ProductDesc", new newLanguageData.columnKeys("~ProductDesc"));
            newLanguageData.colPos.Add("CashProduct_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Key", new newLanguageData.columnKeys("^Key"));
            dictionary.Add("Title", new newLanguageData.columnKeys("~Title"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("Challenge_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("CharName", new newLanguageData.columnKeys("~CharName"));
            dictionary.Add("DisplayName", new newLanguageData.columnKeys("~DisplayName"));
            dictionary.Add("CharacterTitle", new newLanguageData.columnKeys("~CharacterTitle"));
            newLanguageData.colPos.Add("Character_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("CharacterKey", new newLanguageData.columnKeys("^CharacterKey"));
            dictionary.Add("BuyingFromNpcName", new newLanguageData.columnKeys("~BuyingFromNpcName"));
            dictionary.Add("SellingToNpcName", new newLanguageData.columnKeys("~SellingToNpcName"));
            dictionary.Add("TradingName", new newLanguageData.columnKeys("~TradingName"));
            dictionary.Add("ItemMarketName", new newLanguageData.columnKeys("~ItemMarketName"));
            dictionary.Add("BuyingFromGuildShopNpcName", new newLanguageData.columnKeys("~BuyingFromGuildShopNpcName"));
            dictionary.Add("SellingToGuildShopNpcName", new newLanguageData.columnKeys("~SellingToGuildShopNpcName"));
            dictionary.Add("AuctionName", new newLanguageData.columnKeys("~AuctionName"));
            dictionary.Add("MatingName", new newLanguageData.columnKeys("~MatingName"));
            dictionary.Add("InnName", new newLanguageData.columnKeys("~InnName"));
            dictionary.Add("ExploreKeyName", new newLanguageData.columnKeys("~ExploreKeyName"));
            dictionary.Add("SkillName", new newLanguageData.columnKeys("~SkillName"));
            dictionary.Add("RepairName", new newLanguageData.columnKeys("~RepairName"));
            dictionary.Add("WarehouseName", new newLanguageData.columnKeys("~WarehouseName"));
            dictionary.Add("StableName", new newLanguageData.columnKeys("~StableName"));
            dictionary.Add("TransferName", new newLanguageData.columnKeys("~TransferName"));
            dictionary.Add("TransferNamePerson", new newLanguageData.columnKeys("~TransferNamePerson"));
            dictionary.Add("IntimacyName", new newLanguageData.columnKeys("~IntimacyName"));
            dictionary.Add("GuildName", new newLanguageData.columnKeys("~GuildName"));
            dictionary.Add("ExploreName", new newLanguageData.columnKeys("~ExploreName"));
            dictionary.Add("LordMenuName", new newLanguageData.columnKeys("~LordMenuName"));
            dictionary.Add("ExtractName", new newLanguageData.columnKeys("~ExtractName"));
            dictionary.Add("TerritoryTradeName", new newLanguageData.columnKeys("~TerritoryTradeName"));
            dictionary.Add("TerritorySupplyName", new newLanguageData.columnKeys("~TerritorySupplyName"));
            dictionary.Add("KnowledgeName", new newLanguageData.columnKeys("~KnowledgeName"));
            newLanguageData.colPos.Add("CharacterFunction_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("ItemName", new newLanguageData.columnKeys("~ItemName"));
            newLanguageData.colPos.Add("Class_DataSkill_MeshType_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("TerritoryKey", new newLanguageData.columnKeys("^TerritoryKey"));
            dictionary.Add("TerritoryName", new newLanguageData.columnKeys("~TerritoryName"));
            newLanguageData.colPos.Add("ContributionEXP_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("FileName", new newLanguageData.columnKeys("^FileName"));
            dictionary.Add("CutSceneName", new newLanguageData.columnKeys("~CutSceneName"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("CutScene_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Npc", new newLanguageData.columnKeys("!Npc"));
            dictionary.Add("DialogIndex", new newLanguageData.columnKeys("!DialogIndex"));
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("MainDialog", new newLanguageData.columnKeys("~MainDialog"));
            dictionary.Add("Button", new newLanguageData.columnKeys("~Button"));
            dictionary.Add("Dialog", new newLanguageData.columnKeys("~Dialog"));
            dictionary.Add("Bubble1", new newLanguageData.columnKeys("~Bubble1"));
            dictionary.Add("Bubble2", new newLanguageData.columnKeys("~Bubble2"));
            dictionary.Add("Bubble3", new newLanguageData.columnKeys("~Bubble3"));
            dictionary.Add("Bubble4", new newLanguageData.columnKeys("~Bubble4"));
            dictionary.Add("Bubble5", new newLanguageData.columnKeys("~Bubble5"));
            dictionary.Add("Bubble6", new newLanguageData.columnKeys("~Bubble6"));
            dictionary.Add("Bubble7", new newLanguageData.columnKeys("~Bubble7"));
            dictionary.Add("Bubble8", new newLanguageData.columnKeys("~Bubble8"));
            dictionary.Add("Bubble9", new newLanguageData.columnKeys("~Bubble9"));
            dictionary.Add("Bubble10", new newLanguageData.columnKeys("~Bubble10"));
            newLanguageData.colPos.Add("Dialog_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Text", new newLanguageData.columnKeys("~Text"));
            newLanguageData.colPos.Add("DialogText_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Key", new newLanguageData.columnKeys("^Key"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("Encyclopedia_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("EquipSetOptionName", new newLanguageData.columnKeys("~EquipSetOptionName"));
            newLanguageData.colPos.Add("EquipSetOption", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("WaypointKey", new newLanguageData.columnKeys("^WaypointKey"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("Explore_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("FieldNo", new newLanguageData.columnKeys("^FieldNo"));
            dictionary.Add("FieldName", new newLanguageData.columnKeys("~FieldName"));
            newLanguageData.colPos.Add("Field_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("FusionGroup", new newLanguageData.columnKeys("^FusionGroup"));
            dictionary.Add("FusionItemKey", new newLanguageData.columnKeys("^FusionItemKey"));
            dictionary.Add("이 아이템은 무엇인가?", new newLanguageData.columnKeys("~이 아이템은 무엇인가?"));
            dictionary.Add("이 것은 시드 아이템인가?", new newLanguageData.columnKeys("~이 것은 시드 아이템인가?"));
            newLanguageData.colPos.Add("Fusion_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("QuestGroup", new newLanguageData.columnKeys("^QuestGroup"));
            dictionary.Add("GroupName", new newLanguageData.columnKeys("~GroupName"));
            newLanguageData.colPos.Add("Group_Quest", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("GuildQuestNo", new newLanguageData.columnKeys("^GuildQuestNo"));
            dictionary.Add("Title", new newLanguageData.columnKeys("~Title"));
            dictionary.Add("CompleteConditionText", new newLanguageData.columnKeys("~Desc"));  //zamienione z Desc na CompleteConditionText
            dictionary.Add("CompleteCondition", new newLanguageData.columnKeys("~CompleteCondition"));
      //      dictionary.Add("CompleteConditionText", new newLanguageData.columnKeys("~CompleteConditionText"));
            newLanguageData.colPos.Add("GuildQuest_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("ItemName", new newLanguageData.columnKeys("~ItemName"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            dictionary.Add("PopupDesc", new newLanguageData.columnKeys("~PopupDesc"));
            newLanguageData.colPos.Add("Item_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("PlantDescription", new newLanguageData.columnKeys("~PlantDescription"));
            dictionary.Add("PlantDetailDescription", new newLanguageData.columnKeys("~PlantDetailDescription"));
            newLanguageData.colPos.Add("ItemExchangeSource", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("워리어", new newLanguageData.columnKeys("~워리어"));
            dictionary.Add("레인저", new newLanguageData.columnKeys("~레인저"));
            dictionary.Add("소서러", new newLanguageData.columnKeys("~소서러"));
            dictionary.Add("자이언트", new newLanguageData.columnKeys("~자이언트"));
            dictionary.Add("금수랑", new newLanguageData.columnKeys("~금수랑"));
            dictionary.Add("무사", new newLanguageData.columnKeys("~무사"));
            dictionary.Add("매화", new newLanguageData.columnKeys("~매화"));
            dictionary.Add("발키리", new newLanguageData.columnKeys("~발키리"));
            dictionary.Add("쿠노이치", new newLanguageData.columnKeys("~쿠노이치"));
            dictionary.Add("닌자", new newLanguageData.columnKeys("~닌자"));
            dictionary.Add("위자드", new newLanguageData.columnKeys("~위자드"));
            dictionary.Add("위치", new newLanguageData.columnKeys("~위치"));
            newLanguageData.colPos.Add("Item_Exchange", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ActionName", new newLanguageData.columnKeys("^ActionName"));
            dictionary.Add("가공 액션 명", new newLanguageData.columnKeys("~가공 액션 명"));
            newLanguageData.colPos.Add("ManufactureCondition_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("MaterialItem1", new newLanguageData.columnKeys("^MaterialItem1"));
            dictionary.Add("MaterialItemCount1", new newLanguageData.columnKeys("^MaterialItemCount1"));
            dictionary.Add("^MaterialItem2", new newLanguageData.columnKeys("^MaterialItem2"));
            dictionary.Add("MaterialItemCount2", new newLanguageData.columnKeys("^MaterialItemCount2"));
            dictionary.Add("MaterialItem3", new newLanguageData.columnKeys("^MaterialItem3"));
            dictionary.Add("MaterialItemCount3", new newLanguageData.columnKeys("^MaterialItemCount3"));
            dictionary.Add("MaterialItem4", new newLanguageData.columnKeys("^MaterialItem4"));
            dictionary.Add("MaterialItemCount4", new newLanguageData.columnKeys("^MaterialItemCount4"));
            dictionary.Add("MaterialItem5", new newLanguageData.columnKeys("^MaterialItem5"));
            dictionary.Add("MaterialItemCount5", new newLanguageData.columnKeys("^MaterialItemCount5"));
            dictionary.Add("결과물", new newLanguageData.columnKeys("~결과물"));
            newLanguageData.colPos.Add("Manufacture_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("NpcKey", new newLanguageData.columnKeys("^NpcKey"));
            dictionary.Add("CoveredDialog", new newLanguageData.columnKeys("~CoveredDialog"));
            dictionary.Add("DiscoverDialog", new newLanguageData.columnKeys("~DiscoverDialog"));
            dictionary.Add("DiscoverDialog1", new newLanguageData.columnKeys("~DiscoverDialog1"));
            dictionary.Add("DiscoverDialog2", new newLanguageData.columnKeys("~DiscoverDialog2"));
            dictionary.Add("DiscoverDialog3", new newLanguageData.columnKeys("~DiscoverDialog3"));
            dictionary.Add("DiscoverDialog4", new newLanguageData.columnKeys("~DiscoverDialog4"));
            dictionary.Add("DiscoverDialog5", new newLanguageData.columnKeys("~DiscoverDialog5"));
            newLanguageData.colPos.Add("NpcRelation", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("InstallationMaxCount", new newLanguageData.columnKeys("~InstallationMaxCount"));
            dictionary.Add("Desc_Feature1", new newLanguageData.columnKeys("~Desc_Feature1"));
            dictionary.Add("Desc_Feature2", new newLanguageData.columnKeys("~Desc_Feature2"));
            dictionary.Add("ArmorMaterial", new newLanguageData.columnKeys("~ArmorMaterial"));
            newLanguageData.colPos.Add("Object_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ClassType", new newLanguageData.columnKeys("^ClassType"));
            dictionary.Add("ClassName", new newLanguageData.columnKeys("~ClassName"));
            dictionary.Add("ClassDesc", new newLanguageData.columnKeys("~ClassDesc"));
            newLanguageData.colPos.Add("PC_Set_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("PetAction_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("ExchangeGroupName", new newLanguageData.columnKeys("~ExchangeGroupName"));
            newLanguageData.colPos.Add("PlantExchangeGroup_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Key", new newLanguageData.columnKeys("^Key"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("PlantWorkerPassiveSkill_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ItemKey", new newLanguageData.columnKeys("^ItemKey"));
            dictionary.Add("ItemName", new newLanguageData.columnKeys("~ItemName"));
            newLanguageData.colPos.Add("ProductTool_Property", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("DialogKey", new newLanguageData.columnKeys("^DialogKey"));
            dictionary.Add("ProfileMessage", new newLanguageData.columnKeys("~ProfileMessage"));
            newLanguageData.colPos.Add("Profiling", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("QuestGroup", new newLanguageData.columnKeys("^QuestGroup"));
            dictionary.Add("QuestID", new newLanguageData.columnKeys("^QuestID"));
            dictionary.Add("Title", new newLanguageData.columnKeys("~Title"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            dictionary.Add("CompleteDisplay", new newLanguageData.columnKeys("~CompleteDisplay"));
            dictionary.Add("CompleteCondition", new newLanguageData.columnKeys("~CompleteCondition"));
            dictionary.Add("CompleteConditionText", new newLanguageData.columnKeys("~CompleteConditionText"));
            dictionary.Add("AcceptDialog", new newLanguageData.columnKeys("~AcceptDialog"));
            dictionary.Add("ProgressDialog", new newLanguageData.columnKeys("~ProgressDialog"));
            dictionary.Add("CompleteDialog", new newLanguageData.columnKeys("~CompleteDialog"));
            newLanguageData.colPos.Add("Quest_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ReceipeKey", new newLanguageData.columnKeys("^ReceipeKey"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            newLanguageData.colPos.Add("ReceipeForTown_New", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("AreaName", new newLanguageData.columnKeys("~AreaName"));
            dictionary.Add("ReturnPositionName", new newLanguageData.columnKeys("~ReturnPositionName"));
            newLanguageData.colPos.Add("Region_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("RegionMap_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("AreaName", new newLanguageData.columnKeys("~AreaName"));
            dictionary.Add("ReturnPositionName", new newLanguageData.columnKeys("~ReturnPositionName"));
            newLanguageData.colPos.Add("RegionWeather_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("CharacterKey", new newLanguageData.columnKeys("^CharacterKey"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            newLanguageData.colPos.Add("ServantSet_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("SkillNo", new newLanguageData.columnKeys("^SkillNo"));
            dictionary.Add("SkillLevel", new newLanguageData.columnKeys("^SkillLevel"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("Skill_Table_New", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("SkillNo", new newLanguageData.columnKeys("^SkillNo"));
            dictionary.Add("SkillName", new newLanguageData.columnKeys("~SkillName"));
            dictionary.Add("SkillShortName", new newLanguageData.columnKeys("~SkillShortName"));
            dictionary.Add("Control", new newLanguageData.columnKeys("~Control"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("SkillType_Table_New", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("ChattingCommand", new newLanguageData.columnKeys("~ChattingCommand"));
            dictionary.Add("ChattingKeyword", new newLanguageData.columnKeys("~ChattingKeyword"));
            dictionary.Add("ConditionMessage", new newLanguageData.columnKeys("~ConditionMessage"));
            newLanguageData.colPos.Add("SocialAction_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("TerritoryKey", new newLanguageData.columnKeys("^TerritoryKey"));
            dictionary.Add("TerritoryName", new newLanguageData.columnKeys("~TerritoryName"));
            newLanguageData.colPos.Add("SupportPointEXP_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("TerritoryKey", new newLanguageData.columnKeys("^TerritoryKey"));
            dictionary.Add("NationalName", new newLanguageData.columnKeys("~NationalName"));
            dictionary.Add("TerritoryName", new newLanguageData.columnKeys("~TerritoryName"));
            newLanguageData.colPos.Add("Territory_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Theme", new newLanguageData.columnKeys("^Theme"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            newLanguageData.colPos.Add("Theme_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Key", new newLanguageData.columnKeys("^Key"));
            dictionary.Add("Title", new newLanguageData.columnKeys("~Title"));
            dictionary.Add("Description", new newLanguageData.columnKeys("~Description"));
            newLanguageData.colPos.Add("Title_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("BuffDescription", new newLanguageData.columnKeys("~BuffDescription"));
            newLanguageData.colPos.Add("TitleBuff_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("CharacterKey", new newLanguageData.columnKeys("^CharacterKey"));
            dictionary.Add("이름", new newLanguageData.columnKeys("~이름"));
            newLanguageData.colPos.Add("VehicleSkillOwner_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("Index", new newLanguageData.columnKeys("^Index"));
            dictionary.Add("Name", new newLanguageData.columnKeys("~Name"));
            dictionary.Add("Desc", new newLanguageData.columnKeys("~Desc"));
            newLanguageData.colPos.Add("VehicleSkill_Table", dictionary);
            dictionary = new Dictionary<string, newLanguageData.columnKeys>();
            dictionary.Add("ZodiacSignKey", new newLanguageData.columnKeys("^ZodiacSignKey"));
            dictionary.Add("ZodiacName", new newLanguageData.columnKeys("~ZodiacName"));
            dictionary.Add("desc", new newLanguageData.columnKeys("~desc"));
            newLanguageData.colPos.Add("ZodiacSign_Table", dictionary);
        }

        public void MakeFile(Dictionary<string, string> translations, MemoryStream bexcel, MemoryStream output)
        {
            this.templateHeaders = new List<newLanguageData.TemplateHeader>();
            BinaryReader binaryReader = new BinaryReader(bexcel);
            uint num = binaryReader.ReadUInt32();
            for (uint num2 = 0u; num2 < num; num2 += 1u)
            {
                binaryReader.ReadBytes((int)(2L * binaryReader.ReadInt64()));
                binaryReader.BaseStream.Position += 4L;
            }
            num = binaryReader.ReadUInt32();
            for (uint num3 = 0u; num3 < num; num3 += 1u)
            {
                this.templateHeaders.Add(new newLanguageData.TemplateHeader(binaryReader, translations));
            }
            ExcelPackage excelPackage = new ExcelPackage();
            excelPackage.Workbook.CreateVBAProject();
            foreach (newLanguageData.TemplateHeader current in from x in this.templateHeaders
                                                                    orderby x.templateName
                                                                    select x)
            {
                if (newLanguageData.colPos.ContainsKey(current.templateName) && current.rowCount > 0)
                {
                    ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(current.templateName);
                    excelWorksheet.Cells[1, 1, current.rowCount + 1, current.columnsExport.Count<string>()].Style.Numberformat.Format = "@";
                    for (int i = 0; i < current.columnsExport.Count<string>(); i++)
                    {
                        excelWorksheet.Cells[1, i + 1].Value = current.columnsExport[i];
                    }
                    ExcelRange excelRange = excelWorksheet.Cells[2, 1, current.rowCount + 1, current.columnsExport.Count<string>()];
                    excelRange.LoadFromDataTable(current.dt, false);
                }
            }
            excelPackage.SaveAs(output);
            excelPackage.Dispose();
            binaryReader.Dispose();
            output.Dispose();
            binaryReader = null;
            output = null;
            excelPackage = null;
        }
    }
}
