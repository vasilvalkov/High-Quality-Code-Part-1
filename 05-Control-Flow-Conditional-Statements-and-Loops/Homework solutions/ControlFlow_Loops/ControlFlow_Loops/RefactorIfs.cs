using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlow_Loops
{
    class RefactorIfs
    {
        void RefactorFirstIf()
        {
            Potato potato;
            //... 
            if (potato != null)
            {
                if (!potato.HasBeenPeeled && !potato.IsRotten)
                {
                    Cook(potato);
                }
            }
        }

        void RefactorSecondIf()
        {
            bool isInRangeY = MIN_Y <= y && y <= MAX_Y;
            bool isInRangeX = MIN_X <= x && x <= MAX_X;

            if (isInRangeX && isInRangeY && shouldVisitCell)
            {
                VisitCell();
            }
        }
    }
}