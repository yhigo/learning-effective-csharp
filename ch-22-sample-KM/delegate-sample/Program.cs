using System;

namespace delegate_sample
{
    class Base { }

    class Derived : Base { }

    delegate Base DelegateBaseReturn();

    delegate void DelegateDerivedParam(Derived x);

    class Program
    {
        static void Main(string[] args)
        {
            Base b;
            b = BaseReturn();       // 同じ型なのでOK
            b = DerivedReturn();    // 基底クラスへのキャストなのでOK

            Derived d = new Derived();
            DerivedParam(d);        // 同じ型なのでOK
            BaseParam(d);           // 基底クラスへのキャストなのでOK

            /**
             * 上記のようにクラスが違っていてもキャストして問題ないよね、と保証されているものについて
             * 戻り値、引数の型に互換性を認めるというのが共変性(covariance)、反変性(contravariance)の考え
             */

            DelegateBaseReturn bd;
            bd = BaseReturn;        // 同じ型なのでOK
            bd += DerivedReturn;    // 戻り値の型は子どもだけど、これもOK   <---    共変性：戻り値の互換性

            DelegateDerivedParam dd;
            dd = DerivedParam;      // 同じ型なのでOK
            dd += BaseParam;        // 引数の型は親だけど、これもOK  <---    反変性：引数の互換性
        }

        static Base BaseReturn() => new Base();

        static Derived DerivedReturn() => new Derived();

        static void BaseParam(Base x) { }

        static void DerivedParam(Derived x) { }
    }


}
