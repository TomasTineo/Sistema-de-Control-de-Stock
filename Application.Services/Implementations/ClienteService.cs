using DTOs.Clientes;
using Domain.Model;
using Data.Repositories;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO?> GetAsync(int id)
        {
            var cliente = await _clienteRepository.GetAsync(id);
            
            if (cliente == null) return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion
            };
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            
            return clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                Telefono = c.Telefono,
                Direccion = c.Direccion
            });
        }

        public async Task<ClienteDTO> CreateAsync(CreateClienteRequest request)
        {
            var cliente = new Cliente(
                0, // El ID se asignará en la base de datos
                request.Nombre,
                request.Apellido,
                request.Email,
                request.Telefono,
                request.Direccion
            );
            
            var clienteCreado = await _clienteRepository.AddAsync(cliente);

            return new ClienteDTO
            {
                Id = clienteCreado.Id,
                Nombre = clienteCreado.Nombre,
                Apellido = clienteCreado.Apellido,
                Email = clienteCreado.Email,
                Telefono = clienteCreado.Telefono,
                Direccion = clienteCreado.Direccion
            };
        }

        public async Task<bool> UpdateAsync(UpdateClienteRequest request)
        {
            var cliente = await _clienteRepository.GetAsync(request.Id);
            if (cliente == null) return false;

            cliente.SetNombre(request.Nombre);
            cliente.SetApellido(request.Apellido);
            cliente.SetEmail(request.Email);
            cliente.SetTelefono(request.Telefono);
            cliente.SetDireccion(request.Direccion);

            return await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _clienteRepository.DeleteAsync(id);
        }
    }
}
