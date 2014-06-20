using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;
using Rhino;
using Rhino.Runtime;
using Rhino.Geometry;
using System.Collections;
using System.Collections.Generic;


namespace Picnic
{
    public class GH_Dict
    {
        public Dictionary<object, object> val;
        public bool initialized;

        public GH_Dict() : base() {
            this.val = new Dictionary<object, object>();
            this.initialized = false;
        }

        public static GH_Dict create(object key, object num)
        {
            GH_Dict dict = new GH_Dict();

            dict.val.Add(key, num);

            return dict;
        }
    }

    public class Picnic
    {
        public List<Dictionary<string, object>> val;
        public bool initialized;

        public Picnic()
            : base() {
            this.val = new List<Dictionary<string, object>>();
            this.initialized = false;
        }

        public int ValCount
        {
            get { return this.val.Count; }
        }

        public List<string> getkeys()
        {
            List<string> keys = new List<string>();

            foreach (string key in this.val[0].Keys)
            {
                keys.Add(key);
            }

            return keys;

        }


        public bool contains(string key)
        {
            //Picnic new_picnic = new Picnic();

            for (int i = 0; i < this.ValCount; i++)
            {
                if (this.val[i].ContainsKey(key)) return true;
            }
            return false;
        }

        public bool add(string key, List<object> vals)
        {
            if (this.ValCount < 1)
            {
                for (int i = 0; i < vals.Count; i++)
                {
                    this.val.Add(new Dictionary<string,object>());
                }
            }

            for (int i = 0; i < this.ValCount; i++)
            {
                if (!this.val[i].ContainsKey(key)) 
                {
                    this.val[i].Add(key, vals[i]);
                }
                else
                {
                    this.val[i][key] = vals[i];
                }
            }
            
            
            return true;
        }
    }
}
