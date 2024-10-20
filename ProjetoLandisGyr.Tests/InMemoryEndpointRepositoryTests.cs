using ProjetoLandisGyr.Repositories;
using ProjetoLandisGyr.Models;
using Xunit;

namespace EndpointManager.Tests
{
    public class InMemoryEndpointRepositoryTests
    {
        private readonly InMemoryEndpointRepository _repository = new();

        // 1. Testes de Adição
        [Fact]
        public void AddEndpoint_ShouldAddSuccessfully()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            Assert.Single(_repository.GetAllEndpoints());
        }

        [Fact]
        public void AddEndpoint_WithDuplicateSerialNumber_ShouldThrowException()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            Assert.Throws<ArgumentException>(() =>
                _repository.AddEndpoint(endpoint));
        }

        [Fact]
        public void AddEndpoint_WithNullEndpoint_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _repository.AddEndpoint(null));
        }

        [Fact]
        public void AddMultipleEndpoints_ShouldAddAllSuccessfully()
        {
            var endpoint1 = new Endpoint("123", 16, 100, "v1.0", 1);
            var endpoint2 = new Endpoint("456", 17, 200, "v1.1", 2);

            _repository.AddEndpoint(endpoint1);
            _repository.AddEndpoint(endpoint2);

            Assert.Equal(2, _repository.GetAllEndpoints().Count);
        }

        // 2. Testes de Remoção
        [Fact]
        public void DeleteEndpoint_ShouldRemoveEndpointSuccessfully()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            var result = _repository.DeleteEndpoint("123");

            Assert.True(result);
            Assert.Empty(_repository.GetAllEndpoints());
        }

        [Fact]
        public void DeleteEndpoint_NonExistingEndpoint_ShouldReturnFalse()
        {
            var result = _repository.DeleteEndpoint("999");

            Assert.False(result);
        }

        [Fact]
        public void DeleteAllEndpoints_ShouldClearRepository()
        {
            var endpoint1 = new Endpoint("123", 16, 100, "v1.0", 1);
            var endpoint2 = new Endpoint("456", 17, 200, "v1.1", 2);

            _repository.AddEndpoint(endpoint1);
            _repository.AddEndpoint(endpoint2);

            _repository.ClearAllEndpoints();

            Assert.Empty(_repository.GetAllEndpoints());
        }

        // 3. Testes de Atualização
        [Fact]
        public void EditSwitchState_ShouldUpdateStateSuccessfully()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            var result = _repository.EditSwitchState("123", 2);

            Assert.True(result);
            Assert.Equal(2, endpoint.SwitchState);
        }

        [Fact]
        public void EditSwitchState_NonExistingSerialNumber_ShouldReturnFalse()
        {
            var result = _repository.EditSwitchState("999", 2);

            Assert.False(result);
        }

        [Fact]
        public void EditSwitchState_WithInvalidState_ShouldThrowException()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _repository.EditSwitchState("123", -1));
        }

        // 4. Testes de Consulta
        [Fact]
        public void GetEndpointBySerialNumber_ShouldReturnCorrectEndpoint()
        {
            var endpoint = new Endpoint("123", 16, 100, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            var result = _repository.FindEndpoint("123");

            Assert.Equal(endpoint, result);
        }

        [Fact]
        public void GetEndpointBySerialNumber_NonExistingSerial_ShouldReturnNull()
        {
            var result = _repository.FindEndpoint("999");

            Assert.Null(result);
        }

        [Fact]
        public void GetAllEndpoints_ShouldReturnAllAddedEndpoints()
        {
            var endpoint1 = new Endpoint("123", 16, 100, "v1.0", 1);
            var endpoint2 = new Endpoint("456", 17, 200, "v1.1", 2);

            _repository.AddEndpoint(endpoint1);
            _repository.AddEndpoint(endpoint2);

            var allEndpoints = _repository.GetAllEndpoints();

            Assert.Equal(2, allEndpoints.Count);
            Assert.Contains(endpoint1, allEndpoints);
            Assert.Contains(endpoint2, allEndpoints);
        }

        // 5. Testes de Limite e Exceção
        [Fact]
        public void AddEndpoint_WithEmptySerialNumber_ShouldThrowException()
        {
            var endpoint = new Endpoint("", 16, 100, "v1.0", 1);

            Assert.Throws<ArgumentException>(() =>
                _repository.AddEndpoint(endpoint));
        }

        [Fact]
        public void ClearAllEndpoints_OnEmptyRepository_ShouldNotThrowException()
        {
            _repository.ClearAllEndpoints();

            Assert.Empty(_repository.GetAllEndpoints());
        }

        [Fact]
        public void AddEndpoint_WithHighConsumption_ShouldAddSuccessfully()
        {
            var endpoint = new Endpoint("123", 16, 10000, "v1.0", 1);
            _repository.AddEndpoint(endpoint);

            Assert.Single(_repository.GetAllEndpoints());
        }

        [Fact]
        public void AddEndpoint_WithSameSerialNumberDifferentCase_ShouldThrowException()
        {
            var endpoint1 = new Endpoint("ABC", 16, 100, "v1.0", 1);
            var endpoint2 = new Endpoint("abc", 16, 100, "v1.0", 1);

            _repository.AddEndpoint(endpoint1);

            Assert.Throws<ArgumentException>(() =>
                _repository.AddEndpoint(endpoint2));
        }

        [Fact]
        public void GetEndpointBySerialNumber_WithWhitespaceSerial_ShouldReturnNull()
        {
            var repository = new InMemoryEndpointRepository();

            var result = repository.FindEndpoint("   ");

            Assert.Null(result);
        }
    
        [Fact]
        public void EditSwitchState_WithWhitespaceSerial_ShouldReturnFalse()
        {
            var result = _repository.EditSwitchState("   ", 2);

            Assert.False(result);
        }
    }
}
