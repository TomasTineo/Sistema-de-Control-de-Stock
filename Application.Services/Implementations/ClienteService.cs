using Application.Services.Interfaces;
using Data.Repositories;
using Domain.Model;
using DTOs.Clientes;
using System.ComponentModel.DataAnnotations;

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


            // Validar nombre
            if (request.Nombre == null) 
            {
                throw new ArgumentNullException("El nombre no puede ser nulo");
            }
            if(request.Nombre.Any(char.IsDigit)) 
            {
                throw new ArgumentException("El nombre no puede contener números");
            }

            // Validar apellido
            if (request.Apellido == null) 
            {
                throw new ArgumentNullException("El apellido no puede ser nulo");
            }
            if(request.Apellido.Any(char.IsDigit)) 
            {
                throw new ArgumentException("El apellido no puede contener números");
            }

            // Validar mail
            var emailAttribute = new EmailAddressAttribute();
            if (!emailAttribute.IsValid(request.Email))
                throw new InvalidOperationException("El email no es válido");


            // Validar telefono
            if (request.Telefono.Any(char.IsLetter))
            {
                throw new InvalidOperationException("El telefono no puede tener letras");
            }

            if(request.Telefono.Length < 7 || request.Telefono.Length > 15) 
            {
                throw new InvalidOperationException("El telefono debe tener entre 7 y 15 dígitos");
            }


            var cliente = new Cliente(
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
