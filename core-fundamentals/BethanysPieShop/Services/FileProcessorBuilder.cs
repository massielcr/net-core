namespace BethanysPieShop.Services
{
    public class FileProcessorBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly List<IFileProcessor> _steps = [];

        public FileProcessorBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public FileProcessorBuilder AddValidator()
        {
            _steps.Add(_serviceProvider.GetRequiredService<FileValidatorProcessor>());
            return this;
        }

        public FileProcessorBuilder AddStorage()
        {
            _steps.Add(_serviceProvider.GetRequiredService<FileStorageProcessor>());
            return this;
        }

        public FileProcessorBuilder AddNotification()
        {
            _steps.Add(_serviceProvider.GetRequiredService<FileNotificationProcessor>());
            return this;
        }

        public IFileProcessor? Build()
        {
            for (int i = 0; i < _steps.Count - 1; i++)
            {
                _steps[i].SetNext(_steps[i + 1]);
            }

            return _steps.Count > 0 ? _steps[0] : null;
        }
    }
}
