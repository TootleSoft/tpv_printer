using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Printer
    {
        public Printer(String address, Int32 port) {
            this.address = address;
            this.port = port;
        }

        public Printer()
        {

        }

        private string _address;

        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        private int _port;

        public int port
        {
            get { return _port; }
            set { _port = value; }
        }


    }
}
