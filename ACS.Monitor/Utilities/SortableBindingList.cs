﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ACS.Monitor.Utilities
{
    public class SortableBindingList<T> : BindingList<T> where T : class
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList()
        {
        }

        public SortableBindingList(IList<T> list)
            : base(list)
        {
        }

        // Gets a value indicating whether the list supports sorting.
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        // Gets a value indicating whether the list is sorted.
        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        // Gets the direction the list is sorted.
        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }

        // Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }

        // Removes any sort applied with ApplySortCore if sorting is implemented
        protected override void RemoveSortCore()
        {
            _sortDirection = ListSortDirection.Ascending;
            _sortProperty = null;
            _isSorted = false; //thanks Luca
        }

        // Sorts the items if overridden in a derived class
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            List<T> list = Items as List<T>;
            if (list == null) return;

            list.Sort(Compare);

            _isSorted = true;
            //fire an event that the list has been changed.
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private int Compare(T lhs, T rhs)
        {
            var result = OnComparison(lhs, rhs);
            //invert if descending
            if (_sortDirection == ListSortDirection.Descending)
                result = -result;
            return result;
        }

        private int OnComparison(T lhs, T rhs)
        {
            object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
            object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
            if (lhsValue == null)
            {
                return (rhsValue == null) ? 0 : -1; //nulls are equal
            }
            if (rhsValue == null)
            {
                return 1; //first has value, second doesn't
            }
            if (lhsValue is IComparable)
            {
                return ((IComparable)lhsValue).CompareTo(rhsValue);
            }
            if (lhsValue.Equals(rhsValue))
            {
                return 0; //both are the same
            }
            //not comparable, compare ToString
            return lhsValue.ToString().CompareTo(rhsValue.ToString());
        }
    }
}