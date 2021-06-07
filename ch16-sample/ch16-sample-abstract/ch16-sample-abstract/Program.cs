using System;

namespace ch16_sample_abstract
{

    abstract class B
    {
        protected B()
        {
            FuncB();
        }

        abstract protected void FuncB();
    }

    class Derived : B
    {
        private readonly string msg = "初期化子で決定";

        public Derived(string msg)
        {
            this.msg = msg;
        }

        protected override void FuncB()
        {
            Console.WriteLine(msg);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var derived = new Derived("called by main");
        }
    }
}
