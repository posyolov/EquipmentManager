using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class FilterCriteriaInterval<T> : FilterCriteria where T : IComparable
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

        public bool Include(T value)
        {
            if (!_enabled || _criteriaBegin == null && _criteriaEnd == null)
                return true;

            if (value == null)
                return false;

            bool result = true;

            if (_criteriaBegin != null && value.CompareTo(_criteriaBegin) < 0 )
                result = false;
            if (_criteriaEnd != null && value.CompareTo(_criteriaEnd) > 0)
                result = false;

            return result;
        }
    }
}
