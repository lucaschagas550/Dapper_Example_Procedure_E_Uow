using DapperExample.Models;
using DapperExample.Repository.Interfaces;
using DapperExample.Service.Interfaces;

namespace DapperExample.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                return await _repository.GetCompanies();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            try
            {
                return await _repository.GetCompanyById(id).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Company> GetCompanyByIdWithParameters(int id, string name)
        {
            try
            {
                return await _repository.GetCompanyByIdWithParameters(id, name).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<int> CreateCompany(Company company)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var result = await _repository.CreateCompany(company).ConfigureAwait(false);

                //throw new Exception("Erro triste erro");

                _unitOfWork.Commit();

                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task UpdateComapny(Company company)
        {
            try
            {
                await _repository.UpdateCompany(company).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
