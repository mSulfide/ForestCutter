namespace RandMath
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rand
    {
        public static Vector3 GetPosition(float outRange, float inRange = 0f)
        {
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            float distance = Mathf.Sqrt(Random.Range(inRange / outRange, 1f)) * outRange;
            return new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * distance;
        }

        public static Quaternion GetRotation() => Quaternion.Euler(Vector3.up * Random.Range(-180f, 180f));
    }

    [System.Serializable]
    public class Range
    {
        [SerializeField] private float _a, _b;

        public Range(float a, float b)
        {
            _a = a;
            _b = b;
        }

        public static implicit operator float(Range range)
        {
            float a = range._a;
            float b = range._b;
            return a < b ? Random.Range(a, b) : Random.Range(b, a);
        }
    }

    [System.Serializable]
    public class RandomValue<T> : IEnumerable<T>
    {
        [System.Serializable]
        public class ValueWeigth
        {
            [SerializeField] private T _value;
            [SerializeField, Min(0f)] private float _weight;

            public T Value => _value;
            public float Weight => _weight;
        }

        [SerializeField] private List<ValueWeigth> _values;

        public static implicit operator T(RandomValue<T> level)
        {
            float sum = level.GetSum();
            if (sum == 0f) return default;
            float arrow = Random.Range(0f, sum);
            foreach (var value in level._values)
            {
                arrow -= value.Weight;
                if (arrow < 0f)
                    return value.Value;
            }
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in _values)
                yield return value.Value;
        }

        private float GetSum()
        {
            float sum = 0;
            foreach (var value in _values)
                sum += Mathf.Abs(value.Weight);
            return sum;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}