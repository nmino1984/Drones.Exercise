using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Drones.Utilities.Statics
{
    public class ReplyMessages
    {
        public const string MESSAGE_QUERY = "Succeed Query!";
        public const string MESSAGE_QUERY_EMPTY = "No Items found!";
        public const string MESSAGE_SAVE = "Item Saved Successfully";
        public const string MESSAGE_UPDATE = "Item Updated Successfully";
        public const string MESSAGE_DELETE = "Item Deleted Successfully";
        public const string MESSAGE_EXIST = "Item Exists already.";
        public const string MESSAGE_ACTIVATE = "Item has been Activated";
        public const string MESSAGE_TOKEN = "Token generated Successfully";
        public const string MESSAGE_VALIDATE = "Errors on the Validation";
        public const string MESSAGE_FAILED = "Operation Failed";

        public const string MESSAGE_NOT_DRONES_IN_FLEET = "There are no Drones in the Fleet";
        public const string MESSAGE_ALREADY_10_DRONES_IN_FLEET = "There alreagy 10 (ten) Drones in the Fleet. Only 10 Drones allowed.";
        public const string MESSAGE_MORE_THAN_10_DRONES_IN_FLEET = "There are more than 10 (ten) Drones in the Fleet. Will be shown first 10 in Database";
        public const string MESSAGE_WRONG_DRONE_ID = "The Drone Id given was not found!";
        public const string MESSAGE_EMPTY_MEDICATIONS = "There are no Medications to Load the Drone!";
        public const string MESSAGE_DRONE_NOT_LOADED = "The Drone is Not Loaded!";
        public const string MESSAGE_NOT_DRONE_AVAILABLE = "Not Drone Available at this moment!";
        public const string MESSAGE_SELECTED_DRONE_NOT_AVAILABLE = "The Selected Drone is Not Available at this moment!";
        public const string MESSAGE_DRONE_BATTERY_LEVEL = "Drone Battery level retrieve Successfully.";
        public const string MESSAGE_DRONE_BATTERY_LEVEL_ERROR = "There was an Error Retrieving Drone Battery level.";
        public const string MESSAGE_TOO_HEAVY_LOAD_FOR_SELECTED_DRONE = "There is a too heavy Load for the Selected Drone.";
    }

    public static class Validations
    {
        public static bool ValidateMedicationName(string name)
        {
            bool result = Regex.IsMatch(name, @"\A(?:[a-z_-])");
            return result;
        }

    }
}
