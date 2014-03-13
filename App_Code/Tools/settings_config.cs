namespace settings_appl
{
    public class settings_config
    {
        public settings_config()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Course_Management_By{
            Sub_Category = 1,
            Course = 2,
            Custom_Line = 3
        }
        public enum Counter
        {
            One=1,Two=2,Three=3,Four=4,Five=5,Six=6,Seven=7,Eight=8,Nine=9,Ten=10
        }

        public enum Quick_Service
        {
            TAKE_AWAY=1, BAR=2, QUICK_SERVICE=3,RETAIL=4
        }
        public enum TableLayoutType
        { 
            Large_Table_Layout = 1, Small_Table_Layout=2, iPod_iPhone_Table_Layout = 3
        }

        public enum SortItemBy
        {
            Alphabetically=1, Sort_Order=2
        }

        public enum ScannerModeOptions
        {
            Disabled = 0,
            Device_Camera = 1,
            Bluetooth_Scanner = 2
        }


        public enum HoldAndFireOptions
        {
            FIRE = 0,
            HOLD = 1,
        }
    
    }
}