using System;
using System.Collections;

namespace ch_19_ienumerator
{

    /**
     * memo
     * ・IEnumerableは、型が列挙可能(foreach可能)であることを表すためのインターフェイス
     * ・IEnumeratorはコレクション内の要素を列挙（foreachから取得）する機能を提供するインターフェイス
     * 　
     * ・foreach処理中に呼び出される
     * 　→　反復対象のコレクションを最初に参照したとき・・・IEnumeratorを実装しているクラスの「コンストラクタ」が実行される
     * 　　　反復対象のコレクションの参照をコピー
     * 　　　これは最初のだけ
     * 　→　in が実行されたとき・・・IEnumeratorを実装しているクラスの「MoveNext()」が実行される
     * 　　　コレクションから値を取り出せるかチェックしている　OKならtrueを返す
     * 　→　コレクションからオブジェクトを取り出すとき・・・IEnumeratorを実装しているクラスの「Current」プロパティが実行される
     * 　　　コレクションから取得対象のオブジェクトを取得する　配列からインデックスを指定して取り出す
     * ・
     */

    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[3]
            {
                new Person("John", "Smith"),
                new Person("Jim", "Johnson"),
                new Person("Sue", "Rabon"),
            };

            People peopleList = new People(peopleArray);
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p.firstName + " " + p.lastName);
            }
        }
    }

    // Simple business object.
    public class Person
    {
        public Person(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
        }

        public string firstName;
        public string lastName;
    }

    // Collection of Person objects. This class
    // implements IEnumerable so that it can be used
    // with ForEach syntax.
    public class People : IEnumerable
    {
        private Person[] _people;
        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
