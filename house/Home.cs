using System;
using System.Collections.Generic;
using System.Text;

namespace house
{
    class Home
    {
        int _id;
        int _kategory;
        string _address;
        int _connect;
        int _limit;
        int _pay;
        public Home(int _id, int _kategory,string _address,int _connect,
            int _limit, int _pay)
        {
            this._id = _id;
            this._kategory = _kategory;
            this._address = _address;
            this._connect = _connect;
            this._limit = _limit;
            this._pay = _pay;
        }
        public int id { get { return _id; } set { _id = value; } }
        public int kategory { get { return _kategory; } set { _kategory = value; } }
        public string address { get { return _address; } set { _address = value; } }
        public int connect { get { return _connect; } set { _connect = value; } }
        public int limit { get { return _limit; } set { _limit = value; } }
        public int pay { get { return _pay; } set { _pay = value; } }
        public override string ToString()
        {
            return " " + _address + ", лимит: " + _limit + ", плата =  " + _pay + "\n";
        }
    }
}
