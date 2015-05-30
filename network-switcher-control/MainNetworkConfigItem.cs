using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace network_switcher_control
{
    public class MainNetworkConfigItem
    {
        public int ID {get; private set;}
        public string ConnectionName {get; private set;}
        public string IPAddress { get; private set; }
        public string NetMask {get; private set;}
        public string DefaultGateway {get; private set;}
        public string PrimaryDNS {get; private set;}
        public string SecondaryDNS {get; private set;}


        public MainNetworkConfigItem()
        {
            this.ID = 0;
            this.ConnectionName = String.Empty;
            this.IPAddress = String.Empty;
            this.NetMask = String.Empty;
            this.DefaultGateway = String.Empty;
            this.PrimaryDNS = String.Empty;
            this.SecondaryDNS = String.Empty;
        }

        public void setID(int id)
        {
            this.ID = id;
        }

        public void setConnectionName(string connectionName)
        {
            this.ConnectionName = connectionName;
        }

        public void setIPAddress(string ipAddr)
        {
            this.IPAddress = ipAddr;
        }

        public void setNetMask(string netMask)
        {
            this.NetMask = netMask;
        }

        public void setDefaultGateway(string defaultGateway)
        {
            this.DefaultGateway = defaultGateway;
        }

        public void setPrimaryDNS(string primaryDNS)
        {
            this.PrimaryDNS = primaryDNS;
        }

        public void setSecondaryDNS(string secondaryDNS)
        {
            this.SecondaryDNS = secondaryDNS;
        }
    }
}
