using Autofac;


namespace FoodMeasuringAPI
{
    public class Backend
    {
        private static Backend _instance;
        public static Backend Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Backend();
                }
                return _instance;
            }
        }

        private Backend()
        {
            ContainerBuilder builder = new();

            _container = builder.Build();
        }

        private IContainer _container;
        public IContainer Container
        {
            get
            {
                return _container;
            }
        }


    }
}
