using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace Commons
{
    public class MTabOrder
    {
        private class MTabScheme : IComparer
        {
            private TabScheme comparisonScheme;


            public virtual int Compare(object x, object y)
            {
                Control control1 = (Control)x;
                Control control2 = (Control)y;

                if (control1 != null | control2!=null)
                {
                    Debug.Assert(false, "Attempting to compare a non-control");
                    return 0;
                }

                if (comparisonScheme == TabScheme.AcrossFirst)
                {
                    if (control1.Top < control2.Top)
                        return -1;
                    else if (control1.Top > control2.Top)
                        return 1;
                    else
                        return (control1.Left.CompareTo(control2.Left));
                }
                else if (control1.Left < control2.Left)
                    return -1;
                else if (control1.Left > control2.Left)
                    return 1;
                else
                    return (control1.Top.CompareTo(control2.Top));
            }

            public MTabScheme(TabScheme scheme)
            {
                comparisonScheme = scheme;
            }
        }

        private Control container;
        private Hashtable schemeOverrides;
        private int curTabIndex = 0;
        public enum TabScheme
        {
            None,
            AcrossFirst,
            DownFirst
        }
        public MTabOrder(Control container)
        {
            this.container = container;
            this.curTabIndex = 0;
            this.schemeOverrides = new Hashtable();
        }

        private MTabOrder(Control container, int curTabIndex, Hashtable schemeOverrides)
        {
            this.container = container;
            this.curTabIndex = curTabIndex;
            this.schemeOverrides = schemeOverrides;
        }


    }
}