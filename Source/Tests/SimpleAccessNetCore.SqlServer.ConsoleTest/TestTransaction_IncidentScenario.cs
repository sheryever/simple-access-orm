using SimpleAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccessNetCore.SqlServer.ConsoleTest
{
    class TestTransaction_IncidentScenario
    {
    }

    /// <summary>
    /// IncidentsService defines the business logic and validations of Incidents in 
    /// </summary>
    public partial class IncidentsService
    {
        private readonly ISqlRepository _repo;

        public IncidentsService() :
            this(new SqlRepository())
        {

        }
        public IncidentsService(ISqlRepository repo)
        {
            _repo = repo;
        }

        #region Incident--Business Logic Methods



        /// <summary>Retrieves all the Incident objects from the Incidents table</summary>
        /// <returns>The IEnumerable List of all the Incident objects</returns>
        public IEnumerable<Incident> GetAllIncidents()
        {
            return _repo.GetAll<Incident>();
        }

        /// <summary>Retrieves a single Incident object defined by Id from Incidents table</summary>
        /// <param name="id">The Primary Key in Incidents</param>
        /// <returns>The Incident object</returns>
        public Incident GetIncidentById(long id)
        {
            return _repo.Get<Incident>(id);
        }

        /// <summary>Creates the Incident object defined by entity in Incidents table</summary>
        /// <param name="entity">The Incident object to be created</param>
        /// <param name="transaction">
        /// 	An optional transaction parameter to be used 
        /// 	if action is to be performed under some transaction.
        /// </param>
        /// <returns></returns>
        public void InsertIncident(Incident entity, SqlTransaction transaction = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"Null {nameof(Incident)} cannot be inserted.");
            }

            if (transaction == null)
            {
                _repo.Insert<Incident>(entity);
            }
            else
            {
                _repo.Insert<Incident>(transaction, entity);
            }
        }


        /// <summary>Creates the Incident object defined by entity in Incidents table</summary>
        /// <param name="entity">The Incident object to be created</param>
        /// <param name="transaction">
        /// 	An optional transaction parameter to be used 
        /// 	if action is to be performed under some transaction.
        /// </param>
        /// <returns></returns>
        public void InsertIncidentSimple(Incident entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"Null {nameof(Incident)} cannot be inserted.");
            }

            using (var transaction = _repo.SimpleAccess.BeginTransaction())
            {
                try
                {
                    _repo.Insert<Incident>(transaction, entity);

                    for (var i = 0; i < 5; i++)
                        _repo.Insert<Attachment>(transaction, new Attachment { Id = i, IncidentId = entity.Id, OtherName = string.Format("Other Name {0}", i) });

                    _repo.SimpleAccess.EndTransaction(transaction);
                }
                catch (Exception)
                {
                    _repo.SimpleAccess.EndTransaction(transaction, false);

                    throw;
                }
            }

        }

        /// <summary>Deletes the Incident object defined by Id from Incidents table</summary>
        /// <param name="id">The Primary Key in Incidents</param>
        /// <param name="transaction">
        /// 	An optional transaction parameter to be used 
        /// 	if action is to be performed under some transaction.
        /// </param>
        /// <returns></returns>
        public void DeleteIncident(long id, SqlTransaction transaction = null)
        {
            int rowAffected;

            if (transaction == null)
            {
                rowAffected = _repo.Delete<Incident>(id);
            }
            else
            {
                rowAffected = _repo.Delete<Incident>(transaction, id);
            }

            if (rowAffected < 1)
            {
                throw new ApplicationException($"No {nameof(Incident)} found to delete.");
            }
        }


        /// <summary>Retrieves all the Id and Name values as dynamic objects from the Incidents table</summary>
        /// <returns>The IEnumerable<dynamic> of Id and Name of all the Incident objects</returns>
        public IEnumerable<dynamic> GetIncidentLookupItems()
        {
            return _repo.SimpleAccess.ExecuteDynamics("dbo.[Incidents_LookupItems]");
        }

        #endregion

    }

    /// <summary>
    /// AttachmentsService defines the business logic and validations of Attachments in 
    /// </summary>
    public partial class AttachmentsService
    {
        private readonly ISqlRepository _repo;

        public AttachmentsService() :
            this(new SqlRepository())
        {

        }
        public AttachmentsService(ISqlRepository repo)
        {
            _repo = repo;
        }

        #region Attachment--Business Logic Methods
        

        /// <summary>Retrieves all the Attachment objects from the Attachments table</summary>
        /// <returns>The IEnumerable List of all the Attachment objects</returns>
        public IEnumerable<Attachment> GetAllAttachments()
        {
            return _repo.GetAll<Attachment>();
        }

        /// <summary>Retrieves a single Attachment object defined by Id from Attachments table</summary>
        /// <param name="id">The Primary Key in Attachments</param>
        /// <returns>The Attachment object</returns>
        public Attachment GetAttachmentById(long id)
        {
            return _repo.Get<Attachment>(id);
        }

        /// <summary>Creates the Attachment object defined by entity in Attachments table</summary>
        /// <param name="entity">The Attachment object to be created</param>
        /// <param name="transaction">
        /// 	An optional transaction parameter to be used 
        /// 	if action is to be performed under some transaction.
        /// </param>
        /// <returns></returns>
        public void InsertAttachment(Attachment entity, SqlTransaction transaction = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"Null {nameof(Attachment)} cannot be inserted.");
            }

            if (transaction == null)
            {
                _repo.Insert<Attachment>(entity);
            }
            else
            {
                _repo.Insert<Attachment>(transaction, entity);
            }
        }
        

        /// <summary>Deletes the Attachment object defined by Id from Attachments table</summary>
        /// <param name="id">The Primary Key in Attachments</param>
        /// <param name="transaction">
        /// 	An optional transaction parameter to be used 
        /// 	if action is to be performed under some transaction.
        /// </param>
        /// <returns></returns>
        public void DeleteAttachment(long id, SqlTransaction transaction = null)
        {
            int rowAffected;

            if (transaction == null)
            {
                rowAffected = _repo.Delete<Attachment>(id);
            }
            else
            {
                rowAffected = _repo.Delete<Attachment>(transaction, id);
            }

            if (rowAffected < 1)
            {
                throw new ApplicationException($"No {nameof(Attachment)} found to delete.");
            }
        }


        #endregion

    }
}
