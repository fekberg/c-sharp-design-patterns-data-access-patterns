using System;

namespace MyShop.Domain.ValueHolders
{
    public interface IValueHolder<T>
    {
        T GetValue(object parameter);
    }

    public class ValueHolder<T> : IValueHolder<T>
    {
        private T value;

        private readonly Func<object, T> getValue;

        public ValueHolder(Func<object, T> getValue)
        {
            this.getValue = getValue;
        }

        public T GetValue(object parameter)
        {
            if(value == null)
            {
                value = getValue(parameter);
            }

            return value;
        }
    }
}
