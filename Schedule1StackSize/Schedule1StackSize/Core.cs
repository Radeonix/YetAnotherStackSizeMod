using Il2CppScheduleOne.Delivery;
using Il2CppScheduleOne.ItemFramework;
using HarmonyLib;
using MelonLoader;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.MelonLogger;
using Il2CppScheduleOne.ObjectScripts;
using Il2CppScheduleOne.UI.Phone.Delivery;
using Il2CppFishNet.Connection;
using UnityEngine;
using static UnityEngine.InputSystem.Layouts.InputDeviceBuilder;
using System.Runtime.CompilerServices;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppScheduleOne.DevUtilities;
using Il2CppFluffyUnderware.DevTools.Extensions;
using Il2CppScheduleOne.Money;
using Il2CppScheduleOne.NPCs;
using MelonLoader.Utils;
using Il2CppJetBrains.Annotations;
using static Il2CppScheduleOne.AvatarFramework.Customization.CharacterCreator;
using Il2CppSystem.Collections.Specialized;
using Il2CppScheduleOne.UI.Shop;

[assembly: MelonInfo(typeof(Schedule1StackSize.StackSize1), "Schedule1StackSize", "1.0.0", "Radeonix", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace Schedule1StackSize
{
    public static class Config
    {
        public static string config_path = Path.Combine(MelonEnvironment.UserDataDirectory, "Stack_Size");
        private static MelonPreferences_Category StackSizeCategory;
        private static MelonPreferences_Entry<int> MiscStackSize;
        private static MelonPreferences_Entry<int> ProductStackSize;
        private static MelonPreferences_Entry<int> IngredientStackSize;
        private static MelonPreferences_Entry<int> GrowingStackSize;
        private static MelonPreferences_Entry<int> PackagingStackSize;
        private static MelonPreferences_Entry<int> ToolsStackSize;
        private static MelonPreferences_Entry<int> LightingStackSize;
        private static MelonPreferences_Entry<int> EquipmentStackSize;
        private static MelonPreferences_Entry<int> FurnitureStackSize;
        private static MelonPreferences_Category Mixer;
        private static MelonPreferences_Entry<int> MixerStackSize;
        private static MelonPreferences_Entry<int> MixerTimePerItem;
        private static MelonPreferences_Entry<bool> MixerEnable;
        private static MelonPreferences_Category DryingRack;
        private static MelonPreferences_Entry<int> DryingRackSize;
        private static MelonPreferences_Entry<bool> DryingRackEnable;

        public static void CreateDirectory()
        {
            Directory.CreateDirectory(config_path);
            Init();
        }

        public static void Init()
        {
            StackSizeCategory = MelonPreferences.CreateCategory("Stack Size Category");
            MiscStackSize = StackSizeCategory.CreateEntry<int>("MiscStackSize", 60);
            ProductStackSize = StackSizeCategory.CreateEntry<int>("ProductStackSize", 60);
            IngredientStackSize = StackSizeCategory.CreateEntry<int>("IngredientStackSize", 60);
            GrowingStackSize = StackSizeCategory.CreateEntry<int>("GrowingStackSize", 60);
            PackagingStackSize = StackSizeCategory.CreateEntry<int>("PackagingStackSize", 60);
            ToolsStackSize = StackSizeCategory.CreateEntry<int>("ToolsStackSize", 60);
            LightingStackSize = StackSizeCategory.CreateEntry<int>("LightingStackSize", 60);
            EquipmentStackSize = StackSizeCategory.CreateEntry<int>("EquipmentStackSize", 60);
            FurnitureStackSize = StackSizeCategory.CreateEntry<int>("FurnitureStackSize", 60);

            Mixer = MelonPreferences.CreateCategory("Mixer Category");
            MixerStackSize = Mixer.CreateEntry<int>("OutputSize", 60);
            MixerTimePerItem = Mixer.CreateEntry<int>("MixerTimePerItem", 1);
            MixerEnable = Mixer.CreateEntry<bool>("Enable", true);

            DryingRack = MelonPreferences.CreateCategory("DryingRack Category");
            DryingRackSize = DryingRack.CreateEntry<int>("OutputSize", 60);
            DryingRackEnable = DryingRack.CreateEntry<bool>("Enable", true);

            StackSizeCategory.SetFilePath(Path.Combine(config_path, "Stack_Size.cfg"));
            StackSizeCategory.SaveToFile();
            Mixer.SetFilePath(Path.Combine(config_path, "Stack_Size.cfg"));
            Mixer.SaveToFile();
            DryingRack.SetFilePath(Path.Combine(config_path, "Stack_Size.cfg"));
            DryingRack.SaveToFile();
        }

        public static int get_MiscStackSize()
        {
            return MiscStackSize.Value;
        }
        public static int get_ProductStackSize()
        {
            return ProductStackSize.Value;
        }
        public static int get_IngredientStackSize()
        {
            return IngredientStackSize.Value;
        }
        public static int get_GrowingStackSize()
        {
            return GrowingStackSize.Value;
        }
        public static int get_PackagingStackSize()
        {
            return PackagingStackSize.Value;
        }
        public static int get_ToolsStackSize()
        {
            return ToolsStackSize.Value;
        }
        public static int get_LightingStackSize()
        {
            return LightingStackSize.Value;
        }
        public static int get_EquipmentStackSize()
        {
            return EquipmentStackSize.Value;
        }
        public static int get_FurnitureStackSize()
        {
            return FurnitureStackSize.Value;
        }



        public static int get_MixerStackSize()
        {
            return MixerStackSize.Value;
        }

        public static bool get_MixerEnable()
        {
            return MixerEnable.Value;
        }

        public static int get_MixerTimePerItem()
        {
            return MixerTimePerItem.Value;
        }

        public static int get_DryingRackSize()
        {
            return DryingRackSize.Value;
        }

        public static bool get_DryingRackEnable()
        {
            return DryingRackEnable.Value;
        }

    }

    [HarmonyPatch]
    public class StackSize1 : MelonMod
    {
        private static int MiscStackSize;
        private static int ProductStackSize;
        private static int IngredientStackSize;
        private static int GrowingStackSize;
        private static int ToolsStackSize;
        private static int PackagingStackSize;
        private static int FurnitureStackSize;
        private static int LightingStackSize;
        private static int EquipmentStackSize;
        private static bool MixerEnable;
        private static int MixerStackSize;
        private static int MixerTimePerItem;
        private static bool DryingRackEnable;
        private static int DryingRackSize;

        public override void OnInitializeMelon()
        {
            Config.CreateDirectory();
            MiscStackSize = Config.get_MiscStackSize();
            ProductStackSize = Config.get_ProductStackSize();
            IngredientStackSize = Config.get_IngredientStackSize();
            GrowingStackSize = Config.get_GrowingStackSize();
            ToolsStackSize = Config.get_ToolsStackSize();
            PackagingStackSize = Config.get_PackagingStackSize();
            FurnitureStackSize = Config.get_FurnitureStackSize();
            LightingStackSize = Config.get_LightingStackSize();
            EquipmentStackSize = Config.get_EquipmentStackSize();

            MixerEnable = Config.get_MixerEnable();
            MixerStackSize = Config.get_MixerStackSize();
            MixerTimePerItem = Config.get_MixerTimePerItem();
            DryingRackEnable = Config.get_DryingRackEnable();
            DryingRackSize = Config.get_DryingRackSize();
            LoggerInstance.Msg("Initialized.");
        }

        //Delivery Vehicle filling
        [HarmonyPatch(typeof(DeliveryVehicle), "Activate")]
        [HarmonyPrefix]
        public static void DeliveryFix(DeliveryVehicle __instance, ref DeliveryInstance instance)
        {
            var OrderedItemTuple = instance.Items;
            MelonLogger.Msg("Delivery Sent from " + instance.StoreName);
            var LVehicle = __instance.Vehicle;
            //RETREIVES THE STORAGE
            var DeliveryStorage = __instance.Vehicle.Storage;
            MelonLogger.Msg("Delivery Storage accessed.");

            //Type: List<ItemInstance>
            var items1 = DeliveryStorage.GetAllItems();

            var UniqueItems = 0;
            string tempItem = "";
            for (int i = 0; i < items1.Count; i++)
            {
                if (tempItem == items1[i].ID)
                {
                    MelonLogger.Msg("Duplicate of unique item not counted towards unique items.");
                } else
                {
                    tempItem = items1[i].ID;
                    UniqueItems++;
                }
            }

            MelonLogger.Msg("number of unique items ordered: " + OrderedItemTuple.Length);
            MelonLogger.Msg("number of real unique items delivered: " + UniqueItems);
            string[] NameOfBadItem = new string[DeliveryShop.DELIVERY_VEHICLE_SLOT_CAPACITY];
            string NameOfBadItemExample = "";
            int QuantityOfBadItems = 0;
            if (OrderedItemTuple.Length != UniqueItems) 
            {
                MelonLogger.Msg("Item count error detected!");

                MelonLogger.Msg("Missing Unique Item in delivery van detected. Attempting to remove the item, refund it, and deliver the rest of the items.");
                //x is the index of the actually delivered goods
                //i is the index of the unique items ordered
                int x = 0;
                string DuplicateItem = "";
                for ( int i = 0;  i < OrderedItemTuple.Length; i++ )
                {
                    
                    MelonLogger.Msg("index of the unique ordered items is: " + i);
                    MelonLogger.Msg("index of the actually delivered goods is: " + x);
                    MelonLogger.Msg(OrderedItemTuple[i].String + " Unique Item");
                    MelonLogger.Msg(items1[x].ID + " Real Item");
                    //This only occurs if the game correctly delivers more than one stack of an item
                    if (DuplicateItem == items1[x].ID)
                    {
                        //skip it and progress to next loop
                        MelonLogger.Msg("Duplicate real unique item skipped and not counted.");
                        i--;
                        x++;
                    }
                    //if unique item does not match ordered item, remove it (IE. a unique item that was ordered had zero real instances in the actual delivery)
                    else if (OrderedItemTuple[i].String != items1[x].ID)
                    {
                        MelonLogger.Msg("Error! Expected item not visible in real delivery vehicle: " + OrderedItemTuple[i].String);
                        MelonLogger.Msg("Removing " + OrderedItemTuple[i].String + " from delivery.");
                        //store the bad item name in an array, position is irrelevant as most of the array is null anyways
                            //I suppose I could have used a stack here and then not had to deal with null sections of an array
                        NameOfBadItem[i] = OrderedItemTuple[i].String;
                        QuantityOfBadItems += OrderedItemTuple[i].Int;
                        NameOfBadItemExample = OrderedItemTuple[i].String;
                        OrderedItemTuple.RemoveAt(i);
                    }
                    else
                    {
                        if (items1.Count-1 != x)
                        {
                            x++;
                        }
                        //store touched real item
                        DuplicateItem = OrderedItemTuple[i].String;

                    }
                        
                }
                MelonLogger.Msg("I am now going to refund as best I can without knowing the value of the lost items. This is at an average of $10 per item and the $200 delivery fee.");
                float RefundAmount = (QuantityOfBadItems * 10) + 200;
                MoneyManager.instance.CreateOnlineTransaction("Refund", 1, RefundAmount, "Refund");

                if (instance.StoreName == "Dan's Hardware")
                {
                    NPCManager.GetNPC("dan_samwell").SendTextMessage("Sorry about that last delivery, lad. Some rascals got into it at a stop light and ran off with some of your goods. " +
                        "I've refunded your delivery fee and a portion of the item costs: about $" + RefundAmount + "\n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod"); 
                } else if (instance.StoreName == "Handy Hank's Hardware")
                {
                    NPCManager.GetNPC("hank_stevenson").SendTextMessage("Looks like I missed some of the goods in your latest delivery. " +
                        "I've refunded your delivery fee and a portion of the item costs: about $" + RefundAmount + "\n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Gas-Mart (West)")
                {
                    NPCManager.GetNPC("chloe_bowers").SendTextMessage("Soooooooooo looks like we lost some of your ingredients on the way to the delivery spot. Sorry, I guess. " +
                        "I gave you a refund and whatever for the delivery fee and some of the items: about $" + RefundAmount + "\n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Gas-Mart (Central)")
                {
                    NPCManager.GetNPC("chloe_bowers").SendTextMessage("So like... some guy kinda just took some of your goods out of our delivery van before it got to you. Sorry, I guess. " +
                        "I gave you a refund and whatever for the delivery fee and some of the items: about $" + RefundAmount + " \n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Oscar’s Store")
                {
                    NPCManager.GetNPC("oscar_holland").SendTextMessage("Hey uh, we lost some of your ingredients. I swear this isn't normal around here..." +
                        "I already paid your delivery fee back to you plus some extra dough for not delivering some stuff: about $" + RefundAmount + " \n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                } else
                {
                    NPCManager.GetNPC("benji_coleman").SendTextMessage("Hey Boss, I just saw some guy jump your delivery driver on the way to the delivery drop off. I'd make sure everything is there..." +
                        "He's definitely not a buddy of mine... but I chipped in for your delivery fee and some of the lost items: : about $" + RefundAmount + " \n\nIn the future, try a different quantity of " + NameOfBadItemExample + ". " +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
            }

            //Quantities of all the ordered items
            int y = 0;
            int[] QuantityOfItem = new int[items1.Count];
            for (int i = 0; i < OrderedItemTuple.Length; i++)
            {
                //if it finds the name of a bad item in the array, skip method
                if (Array.IndexOf(NameOfBadItem, OrderedItemTuple[i].String) == -1)
                {
                    QuantityOfItem[y] = OrderedItemTuple[i].Int;
                    MelonLogger.Msg("Ordered quantity of " + OrderedItemTuple[i].String + ": " + QuantityOfItem[y]);
                    y++;
                } else
                {
                    MelonLogger.Msg("Bad Item detected, keeping place in array.");
                }
                
            }

            //Make a copy of all iteminstance according to the number of items ordered
            var ItemsToAdd = new Il2CppSystem.Collections.Generic.List<ItemInstance>(items1.Count);
            for (int i = 0; i < UniqueItems; i++) ItemsToAdd.Add(null);

            int k = 0;
            for (int i = 0; i < UniqueItems; i++)
            {
                if (i > 0 && ItemsToAdd[i-1].ID == items1[k].ID) {
                    MelonLogger.Msg("Duplicate real item detected, not adding to list for delivery vehicle: " + items1[i].ID);
                    k++;
                    i--;
                } else
                {
                    MelonLogger.Msg("Unqiue Item copy: " + i);
                    ItemsToAdd[i] = items1[k];
                    MelonLogger.Msg("stored item id: " + ItemsToAdd[i].ID);
                    MelonLogger.Msg("stored item name: " + ItemsToAdd[i].Name);
                    k++;
                }
                
            }
            MelonLogger.Msg("Iteminstance for all unique items copied.");
            


            DeliveryStorage.ClearContents();
            MelonLogger.Msg("Emptied storage of delivery vehicle.");



            //fill storage
            MelonLogger.Msg("Filling storage of delivery vehicle according to the ordered quantities and valid iteminstance's provided by the real delivery: ");
            int NumberOfFullStacks;
            ItemInstance ItemInstanceToAdd;
            for (var i = 0; i < ItemsToAdd.Count; i++)
            {
                MelonLogger.Msg("Loop: " + i);
                var StackSizeOfItem = ItemsToAdd[i].StackLimit;
                MelonLogger.Msg("dynamic stack limit: " + StackSizeOfItem);
                if (QuantityOfItem[i] >= StackSizeOfItem)
                {
                    MelonLogger.Msg("Full item stack(s) detected, adding stack(s) of " + ItemsToAdd[i].ID + " and continuing to scan for the remainder of this item.");
                    NumberOfFullStacks = QuantityOfItem[i] / StackSizeOfItem;
                    for (var j = 0; j < NumberOfFullStacks; j++)
                    {
                        //Need to get a copy of every instance as adding the same instance twice will cause both to disappear
                        ItemInstanceToAdd = ItemsToAdd[i].GetCopy();
                        ItemInstanceToAdd.Quantity = StackSizeOfItem;
                        DeliveryStorage.InsertItem(ItemInstanceToAdd, true);
                    }
                }
                ItemInstanceToAdd = ItemsToAdd[i].GetCopy();
                ItemInstanceToAdd.Quantity = QuantityOfItem[i] % StackSizeOfItem;
                MelonLogger.Msg("Adding " + ItemsToAdd[i].ID + " of quantity " + QuantityOfItem[i] + " to delivery vehicle");
                DeliveryStorage.InsertItem(ItemInstanceToAdd, true);
            }

            MelonLogger.Msg("Vehicle Delivery Arrived.");
        }

        //Delivery Order UI recalculation due to variable stack size
        [HarmonyPatch(typeof(DeliveryShop), "WillCartFitInVehicle")]
        [HarmonyPrefix]
        public static bool VehicleCartSize(DeliveryShop __instance, ref bool __result)
        {
            if (__instance == null) return false;

            //Grab the current cart of items
            var ListOfEntires = __instance.listingEntries;
            int QuantityTools = 0;
            int QuantityIngredients = 0;
            int QuantityAgriculture = 0;
            int QuantityPackaging = 0;
            for (int i = 0; i < ListOfEntires.Count; i++)
            {
                if (ListOfEntires[i].MatchingListing.DoesListingMatchCategoryFilter(EShopCategory.Ingredients)) {
                    QuantityIngredients += ListOfEntires[i].SelectedQuantity;
                } else if (ListOfEntires[i].MatchingListing.DoesListingMatchCategoryFilter(EShopCategory.Tools))
                {
                    QuantityTools += ListOfEntires[i].SelectedQuantity;
                } else if (ListOfEntires[i].MatchingListing.DoesListingMatchCategoryFilter(EShopCategory.Agriculture))
                {
                    QuantityAgriculture += ListOfEntires[i].SelectedQuantity;
                } else if (ListOfEntires[i].MatchingListing.DoesListingMatchCategoryFilter(EShopCategory.Packaging))
                {
                    QuantityPackaging += ListOfEntires[i].SelectedQuantity;
                } 
            }

            //calculate slots used based on dynamic stack size of the above quantities of each category
            var DeliveryVehicleSlotTotal = DeliveryShop.DELIVERY_VEHICLE_SLOT_CAPACITY;

            int SlotsUsedByTools = QuantityTools / ToolsStackSize;
            if ((QuantityTools % ToolsStackSize) > 0) SlotsUsedByTools++;

            int SlotsUsedByIngredients = QuantityIngredients / IngredientStackSize;
            if ((QuantityIngredients % IngredientStackSize) > 0) SlotsUsedByIngredients++;

            int SlotsUsedByAgriculture = QuantityAgriculture / GrowingStackSize;
            if ((QuantityAgriculture % GrowingStackSize) > 0) SlotsUsedByAgriculture++;

            int SlotsUsedByPackaging = QuantityPackaging / PackagingStackSize;
            if ((QuantityPackaging % ToolsStackSize) > 0) SlotsUsedByPackaging++;

            var VehicleUsedSlots = SlotsUsedByTools + SlotsUsedByIngredients + SlotsUsedByAgriculture + SlotsUsedByPackaging;

            if (VehicleUsedSlots <= DeliveryVehicleSlotTotal)
            {
                __result = true;
                return false;
            } else
            {
                __result = false;
                return false;
            }

        }

        //Instant delivery NOT TO BE INCLUDED IN MOD
        [HarmonyPatch(typeof(DeliveryManager), "ReceiveDelivery")]
        [HarmonyPrefix]
        public static void InstantDelivery(NetworkConnection conn, ref DeliveryInstance delivery)
        {
            if (delivery == null) { return;}
            delivery.TimeUntilArrival = 3;
            MelonLogger.Msg("Instant delivery set to 3 seconds for delivery ID: " + delivery.DeliveryID);
        }

        //Stack limit change
        [HarmonyPatch(typeof(ItemInstance), "StackLimit", MethodType.Getter)]
        [HarmonyPostfix]
        public static void StackLimitPatch(ItemInstance __instance, ref int __result)
        {
            if (__instance == null) return;
            //MelonLogger.Msg("Item category: " + __instance.Category.ToString());
            //MelonLogger.Msg("Item ID: " + __instance.ID);

            if (__instance.Category.ToString() == "Consumable")
            {
                __result = IngredientStackSize;
            } else if (__instance.Category.ToString() == "Product")
            {
                __result = ProductStackSize;
            } else if (__instance.Category.ToString() == "Ingredient")
            {
                __result = IngredientStackSize;
            } else if (__instance.Category.ToString() == "Growing")
            {
                __result = GrowingStackSize;
            } else if (__instance.Category.ToString() == "Packaging")
            {
                __result = PackagingStackSize;
            } else if (__instance.Category.ToString() == "Furniture")
            {
                __result = FurnitureStackSize;
            } else if (__instance.Category.ToString() == "Lighting")
            {
                __result = LightingStackSize;
            } else if (__instance.Category.ToString() == "Equipment")
            {
                __result = EquipmentStackSize;
            } else if (__instance.Category.ToString() == "Tools")
            {
                //only change trashbag and not weapons, ammo, or trash grabber
                if (__instance.ID == "trashbag")
                {
                    __result = ToolsStackSize;
                }
            } else
            {
                __result = MiscStackSize;
            }
            
            //MelonLogger.Msg("Setting Stack Limit");
        }

        //Drying Rack limit 
        [HarmonyPatch(typeof(DryingRack), "Awake")]
        [HarmonyPrefix]
        public static void DryingRackLimit(DryingRack __instance)
        {
            if (DryingRackEnable == true)
            {
                __instance.ItemCapacity = DryingRackSize;
                MelonLogger.Msg("Setting Drying Rack capacity");
            }
        }

        //Mixing Station Limit
        [HarmonyPatch(typeof(MixingStation), "Start")]
        [HarmonyPrefix]
        public static void MixingStationLimit(MixingStation __instance)
        {
            if (MixerEnable == true)
            {
                __instance.MixTimePerItem = MixerTimePerItem;
                __instance.MaxMixQuantity = MixerStackSize;
                MelonLogger.Msg("Setting Mixing Station capacity");
            }
        }

    }
}