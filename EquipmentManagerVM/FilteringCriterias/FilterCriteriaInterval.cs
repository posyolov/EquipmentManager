using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class FilterCriteriaInterval<T> : FilterCriteria
    {
        private T _criteriaBegin;
        public T CriteriaBegin
        {
            get => _criteriaBegin;
            set
            {
                _criteriaBegin = value;
                CriteriaChangedInvoke();
            }
        }

        private T _criteriaEnd;
        public T CriteriaEnd
        {
            get => _criteriaEnd;
            set
            {
                _criteriaEnd = value;
                CriteriaChangedInvoke();
            }
        }

        public FilterCriteriaInterval(string title, T begin, T end)
            : base(title)
        {
            _criteriaBegin = begin;
            _criteriaEnd = end;
        }
    }
}
