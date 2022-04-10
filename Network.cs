    // class Network
    /// <summary>
    /// Network related classes
    /// </summary>
    public class Network
    {
        // class Netmask
        /// <summary>
        /// Hold classes related to Netmask incl. CIDR
        /// </summary>
        public class Netmask
        {
            /// <summary>
            /// Hold all mappings
            /// </summary>
            public static List<oNetmask>? Mappings { get; set; } 

            // GetNetmaskFromCIDR (1/2)
            public static oNetmask? GetNetmaskFromCIDR(int cidr) {
                if (Mappings is null) { InitMapping(); }
                if (Mappings is null) {return null;}

                foreach (oNetmask? Map in Mappings)
                {
                    if (cidr == Map.CIDR) { return Map; }
                }
                return null;
            }

            // GetNetmaskFromCIDR (2/2)
            public static oNetmask? GetNetmaskFromCIDR(string cidr)
            {
                return GetNetmaskFromCIDR(int.Parse(cidr));
            }

            // GetCIDRFromNetmask 
            public static oNetmask? GetCIDRFromNetmask(string Netmask)
            {
                if (Mappings is null) { InitMapping(); }
                if (Mappings is null) { return null; }
                
                foreach (oNetmask? Map in Mappings)
                {
                    if (Netmask == Map.Netmask) { return Map; }
                }
                return null;
            }

            // InitMapping
            /// <summary>
            /// Fill the collection Mappings with all CIDR netmasks
            /// </summary>
            private static void InitMapping()
            {
                if (Mappings is null) { Mappings = new List<oNetmask>(); }

                Mappings.Add(new oNetmask(0, "0.0.0.0", addresses: 4294967296));
                Mappings.Add(new oNetmask(1, "128.0.0.0", addresses: 2147483648));
                Mappings.Add(new oNetmask(2, "192.0.0.0", addresses: 1073741824));
                Mappings.Add(new oNetmask(3, "224.0.0.0", addresses: 536870912));
                Mappings.Add(new oNetmask(4, "240.0.0.0", addresses: 268435456));
                Mappings.Add(new oNetmask(5, "248.0.0.0", addresses: 134217728));
                Mappings.Add(new oNetmask(6, "252.0.0.0", addresses: 67108864));
                Mappings.Add(new oNetmask(7, "254.0.0.0",addresses: 33554432));
                Mappings.Add(new oNetmask(8, "255.0.0.0",netmaksClass: eNetmaskClass.A, addresses: 16777216));
                Mappings.Add(new oNetmask(9, "255.128.0.0", addresses: 8388608));
                Mappings.Add(new oNetmask(10, "255.192.0.0", addresses: 4194304));
                Mappings.Add(new oNetmask(11, "255.224.0.0", addresses: 2097152));
                Mappings.Add(new oNetmask(12, "255.240.0.0", addresses: 1048576));
                Mappings.Add(new oNetmask(13, "255.248.0.0", addresses: 524288));
                Mappings.Add(new oNetmask(14, "255.252.0.0", addresses: 262144));
                Mappings.Add(new oNetmask(15, "255.254.0.0", addresses: 131072));
                Mappings.Add(new oNetmask(16, "255.255.0.0", netmaksClass: eNetmaskClass.B, addresses: 65536));
                Mappings.Add(new oNetmask(17, "255.255.128.0", addresses: 32768));
                Mappings.Add(new oNetmask(18, "255.255.192.0", addresses: 16384));
                Mappings.Add(new oNetmask(19, "255.255.224.0", addresses: 8192));
                Mappings.Add(new oNetmask(20, "255.255.240.0", addresses: 4096));
                Mappings.Add(new oNetmask(21, "255.255.248.0", addresses: 2048));
                Mappings.Add(new oNetmask(22, "255.255.252.0", addresses: 1024));
                Mappings.Add(new oNetmask(23, "255.255.254.0", addresses: 512));
                Mappings.Add(new oNetmask(24, "255.255.255.0", netmaksClass: eNetmaskClass.C,addresses: 256));
                Mappings.Add(new oNetmask(25, "255.255.255.128", addresses: 128));
                Mappings.Add(new oNetmask(26, "255.255.255.192", addresses: 64));
                Mappings.Add(new oNetmask(27, "255.255.255.224", addresses: 32));
                Mappings.Add(new oNetmask(28, "255.255.255.240", addresses: 16));
                Mappings.Add(new oNetmask(29, "255.255.255.248", addresses: 8));
                Mappings.Add(new oNetmask(30, "255.255.255.252", addresses: 4));
                Mappings.Add(new oNetmask(31, "255.255.255.254", addresses: 2));
                Mappings.Add(new oNetmask(32, "255.255.255.255", addresses: 1));
            }

            // CalculateNetmaskFromCIDR (DEPRECATED)
            /// <summary>DEPRECATED
            /// Get network-maks from specified CIDR, e.g. '26' which will return '255.255.255.192'
            /// Return mask string parsed to R2.Obj and the 4 maske-octet as byte(4) parsed to R2.Obj2
            /// </summary>
            /// <param name="CIDR">E.g. '24' as in mask '255.255.255.0'</param>
            public static Result2 CalculateNetmaskFromCIDR(string CIDR)
            {
                try
                {
                    int CIDRInt = 32;
                    int index = 0;
                    byte[] CIDROctet = new byte[4] { 0, 0, 0, 0 };

                    for (int i = 1; i <= 32; i++)
                    {
                        if (i > CIDRInt) { break; }
                        if (index == 8) { index = 0; }

                        if (i > 24)
                        {
                            CIDROctet[3] |= (byte)(1 << (7 - index)); //index from 7 til -1
                            index++;
                        }
                        else if (i > 16)
                        {
                            CIDROctet[2] |= (byte)(1 << (7 - index));
                            index++;
                        }
                        else if (i > 8)
                        {
                            CIDROctet[1] |= (byte)(1 << (7 - index));
                            index++;
                        }
                        else
                        {
                            CIDROctet[0] |= (byte)(1 << (7 - index));
                            index++;
                        }
                    }

                    R2.SetP(obj: CIDROctet[0] + "." + CIDROctet[1] + "." + CIDROctet[2] + "." + CIDROctet[3]);
                    R2.Obj2 = CIDROctet;
                    return R2.UpdP(0);
                }
                catch (Exception Ex)
                {
                    return R2.SetP(1, Ex);
                    throw;
                }
            }

            // class oCIDRMapping
            public class oNetmask
            {
                /// <summary>E.g. 26</summary>
                public int CIDR { get; set; }
                /// <summary>E.g. 255.255.255.192</summary>
                public string Netmask { get; set; }
                /// <summary>Belonging netmaks-class the A, B or C</summary>
                public eNetmaskClass NetmaksClass { get; set; }
                /// <summary>E.g. 256 adresses in a CIDR-24 subnet</summary>
                public long Addresses { get; set; }

                //oNetmask
                public oNetmask(int cIDR, string netmask, eNetmaskClass netmaksClass = eNetmaskClass.Unkown, long addresses = 0)
                {
                    CIDR = cIDR;
                    Netmask = netmask;
                    NetmaksClass = netmaksClass;
                    Addresses = addresses;
                }
            }

            // enum eNetmaskClass
            public enum eNetmaskClass {
                Unkown = 0,
                A,
                B,
                C
            }
                
        }            
    }

