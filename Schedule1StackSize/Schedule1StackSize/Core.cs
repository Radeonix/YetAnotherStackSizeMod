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
using Il2CppScheduleOne.Storage;
using Il2CppScheduleOne.ObjectScripts.Cash;

[assembly: MelonInfo(typeof(YetAnotherStackSizeMod.StackSize1), "YetAnotherStackSizeMod", "1.1.0", "Radeonix", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace YetAnotherStackSizeMod
{
    public static class Config
    {
        public static string config_path = Path.Combine(MelonEnvironment.UserDataDirectory, "YetAnotherStackSizeMod");
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
            MiscStackSize = StackSizeCategory.CreateEntry<int>("Stack Size of all other un-included items like future categories", 60);
            ProductStackSize = StackSizeCategory.CreateEntry<int>("Product Stack Size (bagged and unbagged products, jarred products, etc)", 60);
            IngredientStackSize = StackSizeCategory.CreateEntry<int>("Ingredient Stack Size (horse semen, mouth wash, all Consumables like Cuke/Energy Drink, etc)", 60);
            GrowingStackSize = StackSizeCategory.CreateEntry<int>("Growing Stack Size (Fertilizer, seeds, soil, etc)", 60);
            PackagingStackSize = StackSizeCategory.CreateEntry<int>("Packaging Stack Size (Jars/Baggies)", 60);
            ToolsStackSize = StackSizeCategory.CreateEntry<int>("Tools Stack Size (trash bags, all other tools are disallowed from stacking)", 60);
            LightingStackSize = StackSizeCategory.CreateEntry<int>("Lighting Stack Size (Grow lights like LED lights)", 60);
            EquipmentStackSize = StackSizeCategory.CreateEntry<int>("Equipment Stack Size (packaging station, mixing station, etc)", 60);
            FurnitureStackSize = StackSizeCategory.CreateEntry<int>("Furniture Stack Size (table and other various forms of non-table furnitures)", 60);

            Mixer = MelonPreferences.CreateCategory("Mixer Category");
            MixerStackSize = Mixer.CreateEntry<int>("OutputSize", 60);
            MixerTimePerItem = Mixer.CreateEntry<int>("MixerTimePerItem", 1);
            MixerEnable = Mixer.CreateEntry<bool>("Enable Mixer stack size change", true);

            DryingRack = MelonPreferences.CreateCategory("DryingRack Category");
            DryingRackSize = DryingRack.CreateEntry<int>("OutputSize", 60);
            DryingRackEnable = DryingRack.CreateEntry<bool>("Enable Drying Rack stack size change", true);

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

        //Delivery preprocessing to attempt guarantee of real items arriving
        [HarmonyPatch(typeof(DeliveryInstance), "AddItemsToDeliveryVehicle")]
        [HarmonyPrefix]
        public static bool DeliveryAddendumPre(DeliveryInstance __instance, out Il2CppReferenceArray<StringIntPair> __state)
        {
            //simply doing __state = __instance.Items; results in a shallow copy/pointer assignment
            //deep copy required:
            MelonLogger.Msg("Deep copy starting.");
            //the below DeepCopy list does not act like a normal list. It initailizes null items so the count already equals the capacity and makes List<T>.Add() useless...
                //It also does not have a constructor with 0 values???
            var DeepCopy = new Il2CppReferenceArray<StringIntPair>(__instance.Items.Length);
            StringIntPair temp;
            for (int i = 0; i < __instance.Items.Length; i++)
            {
                MelonLogger.Msg(__instance.Items[i].String + " quantity being deep-copied: " + __instance.Items[i].Int);
                temp = new StringIntPair(__instance.Items[i].String, __instance.Items[i].Int);
                DeepCopy[i] = temp;
            }
            MelonLogger.Msg("Deep copy finished.");
            __state = DeepCopy;

            for(int i = 0; i < __instance.Items.Length; i++)
            {
                if (__instance.Items[i].Int % 10 == 0)
                {
                    //Must subtract and not increment as it may use more slots than are available
                    __instance.Items[i].Int--;
                    MelonLogger.Msg(__instance.Items[i].String + " quantity temporarily adjusted to: " + __instance.Items[i].Int);
                }
            }
            return true;
        }

        //Correct Delivery quantity due to prefix tampering of user-ordered quantity
        [HarmonyPatch(typeof(DeliveryInstance), "AddItemsToDeliveryVehicle")]
        [HarmonyPostfix]
        public static void DeliveryAddendumPost(DeliveryInstance __instance, Il2CppReferenceArray<StringIntPair> __state)
        {
            if (__instance == null) { return; }
            MelonLogger.Msg("Restoring quantity of delivery quantities.");
            __instance.Items = __state;
            return;
        }

        //Delivery Vehicle filling
        [HarmonyPatch(typeof(DeliveryVehicle), "Activate")]
        [HarmonyPrefix]
        public static void DeliveryFix(DeliveryVehicle __instance, ref DeliveryInstance instance)
        {
            float RefundAmount = 0;
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

            if (UniqueItems == 0)
            {
                MelonLogger.Msg("Error! No real items delivered!");
                MelonLogger.Msg("Initiating refund at a higher average of $10 per item plus the delivery fee");

                int OrderedItemQuantity = 0;

                for (int i = 0; i < OrderedItemTuple.Length; i++)
                {
                    if (OrderedItemTuple[i].String == "baggie" || OrderedItemTuple[i].String == "trashbag")
                    {
                        //help the accuracy of the refund by having the cheapest item in the game return an accurate number
                        RefundAmount += OrderedItemTuple[i].Int;
                    } else {
                        OrderedItemQuantity += OrderedItemTuple[i].Int;
                    }
                    
                }

                RefundAmount = RefundAmount + (OrderedItemQuantity * 10) + 200;
                MoneyManager.instance.CreateOnlineTransaction("Refund", 1, RefundAmount, "Refund");

                if (instance.StoreName == "Dan's Hardware")
                {
                    NPCManager.GetNPC("dan_samwell").SendTextMessage("Sorry about that last delivery, lad. Some rascals got into it at a stop light and ran off with some of your goods. " +
                        "I've refunded your delivery fee and a portion of the item costs: about $" + RefundAmount + "\n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Handy Hank's Hardware")
                {
                    NPCManager.GetNPC("hank_stevenson").SendTextMessage("Looks like I missed some of the goods in your latest delivery. " +
                        "I've refunded your delivery fee and a portion of the item costs: about $" + RefundAmount + "\n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Gas-Mart (West)")
                {
                    NPCManager.GetNPC("chloe_bowers").SendTextMessage("Soooooooooo looks like we lost some of your ingredients on the way to the delivery spot. Sorry, I guess. " +
                        "I gave you a refund and whatever for the delivery fee and some of the items: about $" + RefundAmount + "\n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Gas-Mart (Central)")
                {
                    NPCManager.GetNPC("chloe_bowers").SendTextMessage("So like... some guy kinda just took some of your goods out of our delivery van before it got to you. Sorry, I guess. " +
                        "I gave you a refund and whatever for the delivery fee and some of the items: about $" + RefundAmount + " \n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else if (instance.StoreName == "Oscar’s Store")
                {
                    NPCManager.GetNPC("oscar_holland").SendTextMessage("Hey uh, we lost some of your ingredients. I swear this isn't normal around here..." +
                        "I already paid your delivery fee back to you plus some extra dough for not delivering some stuff: about $" + RefundAmount + " \n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                else
                {
                    NPCManager.GetNPC("benji_coleman").SendTextMessage("Hey Boss, I just saw some guy jump your delivery driver on the way to the delivery drop off. I'd make sure everything is there..." +
                        "He's definitely not a buddy of mine... but I chipped in for your delivery fee and some of the lost items: : about $" + RefundAmount + " \n\nIn the future, try a different quantity of your ordered items." +
                        "\n\n - Sent from Radeonix's Stack Size mod");
                }
                //exit function
                return;
            }






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
                        //do not extend x past the index bounds of the real array
                        if (items1.Count-1 != x)
                        {
                            x++;
                            //store touched real item
                            DuplicateItem = OrderedItemTuple[i].String;
                        }
                        else
                        {
                            MelonLogger.Msg("End of Real Item array reached, clearing duplicated item storage in order to prevent infinite loop");
                            DuplicateItem = "";
                        }
                        
                       

                    }
                        
                }
                MelonLogger.Msg("I am now going to refund as best I can without knowing the value of the lost items. This is at an average of $6 per item and the $200 delivery fee.");
                RefundAmount = (QuantityOfBadItems * 6) + 200;
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

        //Cash stack size
        //GetCapacityForItem is the wrong method, this is used by handlers moving items into another slot, that slot is the capacity
        [HarmonyPatch(typeof(StorageEntity), "Close")]
        [HarmonyPostfix]
        public static void CashStackSize(StorageEntity __instance)
        {
            if (__instance == null) { return; }
            if (__instance.StorageEntityName.Contains("Briefcase") || __instance.StorageEntityName == "Safe")
            {
                var temp = __instance.GetAllItems();
                float TotalStoredCash = 0;
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].ID == "cash")
                    {
                        MelonLogger.Msg("Cash already detected inside " + temp[i].GetMonetaryValue());
                        TotalStoredCash += temp[i].GetMonetaryValue();
                    } else
                    {
                        MelonLogger.Msg("Non-cash item detected, exiting method without any change to avoid item deletion.");
                        return;
                    }
                }
                MelonLogger.Msg("Clearing Contents");
                __instance.ClearContents();
                MelonLogger.Msg("Total cash: " + TotalStoredCash);

                if (TotalStoredCash != 0)
                {
                    __instance.InsertItem(MoneyManager.instance.GetCashInstance(TotalStoredCash));
                }
                
            }
        }

        //Stack limit change
        [HarmonyPatch(typeof(ItemInstance), "StackLimit", MethodType.Getter)]
        [HarmonyPostfix]
        public static void StackLimitPatch(ItemInstance __instance, ref int __result)
        {
            if (__instance == null) return;
            //MelonLogger.Msg("Item category: " + __instance.Category.ToString());
            //MelonLogger.Msg("Item ID: " + __instance.ID);
            
            //could use a switch case but I personally find those harder to read
            if (__instance.Category.ToString() == "Consumable")
            {
                __result = IngredientStackSize;
                return;
            } else if (__instance.Category.ToString() == "Product")
            {
                __result = ProductStackSize;
                return;
            } else if (__instance.Category.ToString() == "Ingredient")
            {
                __result = IngredientStackSize;
                return;
            } else if (__instance.Category.ToString() == "Growing")
            {
                __result = GrowingStackSize;
                return;
            } else if (__instance.Category.ToString() == "Packaging")
            {
                __result = PackagingStackSize;
                return;
            } else if (__instance.Category.ToString() == "Furniture")
            {
                __result = FurnitureStackSize;
                return;
            } else if (__instance.Category.ToString() == "Lighting")
            {
                __result = LightingStackSize;
                return;
            } else if (__instance.Category.ToString() == "Equipment")
            {
                __result = EquipmentStackSize;
                return;
            } else if (__instance.Category.ToString() == "Tools")
            {
                //only change trashbag and not weapons, ammo, or trash grabber
                if (__instance.ID == "trashbag")
                {
                    __result = ToolsStackSize;
                    return;
                }
            } else
            {
                __result = MiscStackSize;
                return;
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