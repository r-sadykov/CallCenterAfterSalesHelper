using System.Linq;
using BERlogic.CallCenter.Data;
using Microsoft.EntityFrameworkCore;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class ServiceOperationsRepository:IServiceOperations
    {
        private readonly ApplicationDbProcessContext _context;
        public ServiceOperationsRepository(ApplicationDbProcessContext context)
        {
            _context = context;
        }
        public IQueryable<ServiceOperationsModel> ServiceOperationsModels => _context.ServiceOperations;
      

        public int Save(ServiceOperationsModel model)
        {
            if (model.Id == 0)
            {
                _context.ServiceOperations.Add(model);
            }
            else
            {
                ServiceOperationsModel dbEntry = _context.ServiceOperations.Include(p=>p.LineRoutes).Include(p=>p.PassangerModels).FirstOrDefault(p => p.Id == model.Id);
                if (dbEntry != null)
                {
                    dbEntry.LineRoutes = model.LineRoutes;
                    dbEntry.AdditionalServicesAndBaggage = model.AdditionalServicesAndBaggage;
                    dbEntry.AgencyCode = model.AgencyCode;

                    dbEntry.AgentName = model.AgentName;
                    dbEntry.AirlineTotalPrice = model.AirlineTotalPrice;

                    dbEntry.BerlogicFee = model.BerlogicFee;
                    dbEntry.BookingCode = model.BookingCode;
                    dbEntry.BookingDate = model.BookingDate;

                    dbEntry.BookingNumber = model.BookingNumber;
                    dbEntry.CancellationAirlineFee = model.CancellationAirlineFee;
                    dbEntry.ChangeServiceDate = model.ChangeServiceDate;

                    dbEntry.ClearingType = model.ClearingType;
                    dbEntry.CustomerEmail = model.CustomerEmail;

                    dbEntry.CustomerFullName = model.CustomerFullName;
                    dbEntry.NonRefundableTaxes = model.NonRefundableTaxes;

                    dbEntry.PassangerModels = model.PassangerModels;

                    dbEntry.PaymentLink = model.PaymentLink;
                    dbEntry.PaymentMethod = model.PaymentMethod;
                    dbEntry.PrepaidBerlogicFee = model.PrepaidBerlogicFee;

                    dbEntry.PriceDifference = model.PriceDifference;
                    dbEntry.RebookingFee = model.RebookingFee;
                    dbEntry.Salespoint = model.Salespoint;

                    dbEntry.ServiceOperations = model.ServiceOperations;
                }
            }
            _context.SaveChanges();
            return model.Id;
        }

        public ServiceOperationsModel Delete(int id)
        {
            ServiceOperationsModel dbEntry = _context.ServiceOperations.Include(p => p.LineRoutes).Include(p => p.PassangerModels).FirstOrDefault(p => p.Id == id);
            if (dbEntry != null)
            {
                _context.ServiceOperations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
