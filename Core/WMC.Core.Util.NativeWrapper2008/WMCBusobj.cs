using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WMC.Core.Util.NativeWrapper2008
{
    /// <summary>
    /// Summary description for WMCBusobj.
    /// </summary>
    public class WMCBusobj
    {

        private ArrayList validationExceptions = null;

        public ArrayList ValidationExceptions { get { return validationExceptions; } set { validationExceptions = value; } }

        public void AddExceptions(ArrayList messages)
        {
            if (messages != null)
            {
                foreach (string message in messages)
                {
                    AddException(message);
                }
            }
        }
        public void AddException(string message)
        {

            if (validationExceptions == null)
            {
                validationExceptions = new ArrayList();
            }

            validationExceptions.Add(message);
        }

        private void ClearExceptions()
        {
            if (validationExceptions != null)
            {
                validationExceptions.Clear();
            }
        }

        public virtual void Validate()
        {
            ClearExceptions();
            //return null;
        }

        public virtual void Validate(int context)
        {
            ClearExceptions();
            //return null;
        }

        public virtual BoolObj isValid()
        {
            ClearExceptions();
            Validate();
            return new BoolObj(validationExceptions == null || validationExceptions.Count < 1);
        }

        public virtual BoolObj isValid(int context)
        {
            ClearExceptions();
            Validate(context);
            return new BoolObj(validationExceptions == null || validationExceptions.Count < 1);
        }

        public override string ToString()
        {
            return "WMCBusobj:  Override the ToString() method to make these results useful.";
        }

        public WMCBusobj()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
