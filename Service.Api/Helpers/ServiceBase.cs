using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using TopTal.JoggingApp.Exceptions;
using TopTal.JoggingApp.Service.Models.Helpers;
using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.BusinessEntities.Helpers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using TopTal.JoggingApp.CallContext;
using TopTal.JoggingApp.DataAccess.Helpers;
using TopTal.JoggingApp.Security.Managers;

namespace TopTal.JoggingApp.Service.Api.Helpers
{
    /// <summary>
    /// Do not add public instance methods here.
    /// </summary>
    public abstract class ServiceBase
    {
        public ServiceBase(
            ICallContext callContext,
            AppConfig appConfig,
            ITransactionManager transactionManager,
            IServiceModelValidator serviceModelValidator,
            IAuthProvider authProvider
            )
        {
            this.CallContext = callContext;
            this.AppConfig = appConfig;
            this.ServiceModelValidator = serviceModelValidator;
            this.TransactionManager = transactionManager;
            this.AuthProvider = authProvider;
        }

        #region Services

        protected IServiceModelValidator ServiceModelValidator { get; private set; }

        protected AppConfig AppConfig { get; private set; }

        protected ICallContext CallContext { get; private set; }

        private ITransactionManager TransactionManager;

        protected IAuthProvider AuthProvider { get; private set; }

        #endregion

        #region Exception Handling 


        /// <summary>
        /// Logs and processes exceptions.
        /// Turns non-user friendly exceptions into HTTP Internal Server Error.
        /// Errors are returned by Http status codes + status text.
        /// Returns HttpStatusCodeResult instance.
        /// See application.js / showError()
        /// </summary>
        protected ServiceResult HandleException(Exception ex)
        {
            // order is important
            if (ex is AppException exApp)
            {
                //if (exApp.LogError) TODO
                //    LogSilent(ex);

                return Error(exApp);
            }
            else if (AppConfig.ServiceApi.ShowDetailedError)
            {
                //LogSilent(ex); TODO
                var result = Error(HttpStatusCode.InternalServerError, ex.Message);
                result.DetailedErrorMessage = ex.ToString();
                return result;
            }
            else
            {
                //LogSilent(ex); TODO
                return Error(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Logs and processes exceptions.
        /// Turns non-user friendly exceptions into HTTP Internal Server Error.
        /// Errors are returned by Http status codes + status text.
        /// Returns HttpStatusCodeResult instance.
        /// See application.js / showError()
        /// </summary>
        protected T HandleException<T>(Exception ex) where T : ServiceResult, new()
        {
            // order is important
            if (ex is AppException exApp)
            {
                //if (exApp.LogError) TODO
                //    LogSilent(ex);

                return Error<T>(exApp);
            }          
            else if (AppConfig.ServiceApi.ShowDetailedError)
            {
                //LogSilent(ex); TODO
                var result = Error<T>(HttpStatusCode.InternalServerError, ex.Message);
                result.DetailedErrorMessage = ex.ToString();
                return result;
            }
            else
            {
                //LogSilent(ex); TODO
                return Error<T>(HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region Service Scope / Antiforgery Token

        /// <summary>
        /// Generates anti-forgery tokens for GET requests
        /// Manages transaction scope (System.Transctions.TransactionScope)
        /// Result is here to validate it inherits AntiForgeryTokenResult and there must be a result to carry the token value (cookie is in response header).
        /// </summary>
        internal ServiceScopeWithResult<TResult> GetContextWithToken<TResult>(bool transactional, TResult result) where TResult : ServiceResultWithToken
        {
            var tokens = this.CallContext.AntiforgeryTokenGenerate();
            result.RequestToken = tokens.RequestToken;

            if (transactional)
            {
                var trans = this.TransactionManager.BeginTransaction();

                return new ServiceScopeWithResult<TResult>(
                    () => { trans.Complete(); },
                    () => { trans.Dispose(); },
                    result
                    );
            }
            else
                return new ServiceScopeWithResult<TResult>(null, null, result);
        }

        /// <summary>
        /// Does not generate anti-forgery tokens for GET requests
        /// Manages transaction scope (System.Transctions.TransactionScope)
        /// </summary>
        internal ServiceScope GetContext(bool transactional)
        {
            if (transactional)
            {
                var trans = this.TransactionManager.BeginTransaction();

                return new ServiceScope(
                    () => { trans.Complete(); },
                    () => { trans.Dispose(); }
                    );
            }
            else
                return new ServiceScope(null, null);
        }

        /// <summary>
        /// Validates anti-forgery tokens in POST requests
        /// Manages transaction scope (System.Transctions.TransactionScope)
        /// </summary>
        internal ServiceScope PostContext(bool transactional, bool validateAntiForgeryToken)
        {
            if (validateAntiForgeryToken)
                this.CallContext.AntiforgeryTokenValidate(true);

            return GetContext(transactional);
        }

        #endregion        

        #region Input Validation

        protected static bool TryParseId(string id, out int intId, out bool isNew)
        {
            if (string.IsNullOrEmpty(id) || id == "new")
            {
                intId = 0;
                isNew = true;
                return true;
            }

            if (int.TryParse(id, out intId))
            {
                isNew = (intId <= 0);
                return true;
            }

            isNew = false;
            return false;
        }

        protected void Expect(Type entityType, object id)
        {
            if (id == null || (id is string && string.IsNullOrEmpty((string)id)))
                throw new Exceptions.Entities.InvalidEntityKeyException(CallContext.ResourceUri, entityType, id);
        }

        protected void Expect<T>(T entity) where T : IDataObject 
        {
            if (entity == null)
                throw new Exceptions.Validation.InputDataMissingException(CallContext.ResourceUri, typeof(T));
        }

        protected void Expect<T>(T entity, object id) where T : IDataObject
        {
            if (entity == null)
                throw new Exceptions.Validation.InputDataMissingException(CallContext.ResourceUri, typeof(T));

            if (id == null || (id is string && string.IsNullOrEmpty((string)id)))
                throw new Exceptions.Entities.InvalidEntityKeyException(CallContext.ResourceUri, typeof(T), id);
        }


        #endregion

        #region Response Messages

        #region OK

        protected ServiceResult OK()
        {
            return new ServiceResult(HttpStatusCode.OK);
        }

        protected T OK<T>(T result) where T : ServiceResult
        {
            result.StatusCode = HttpStatusCode.OK;
            result.StatusDescription = null;

            return result;
        }

        #endregion

        #region Error

        protected ServiceResult Error(HttpStatusCode error)
        {
            return new ServiceResult(error);
        }

        protected ServiceResult Error(HttpStatusCode error, string description)
        {
            return new ServiceResult(error, description);
        }

        protected T Error<T>(HttpStatusCode error) where T : ServiceResult, new()
        {
            return new T()
            {
                StatusCode = error
            };
        }

        protected T Error<T>(HttpStatusCode error, string description) where T : ServiceResult, new()
        {
            return new T()
            {
                StatusCode = error,
                StatusDescription = description
            };
        }

        protected ServiceResult Error(AppException ex)
        {
            var result = new ServiceResult(ex.StatusCode, ex.Description);
            result.DetailedErrorMessage = ex.DetailerErrorMessage;
            return result;
        }

        protected T Error<T>(AppException ex) where T : ServiceResult, new()
        {
            var result = Error<T>(ex.StatusCode, ex.Description);
            result.DetailedErrorMessage = ex.DetailerErrorMessage;
            return result;
        }

        #endregion

        #region BadRequest

        protected ServiceResult BadRequest()
        {
            return new ServiceResult(HttpStatusCode.BadRequest);
        }

        protected T BadRequest<T>() where T : ServiceResult, new()
        {
            return new T()
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        #endregion

        #region ValidationError

        protected ServiceResult ValidationError(string validationMessage)
        {
            return new ServiceResult(HttpStatusCode.BadRequest, validationMessage);
        }

        protected ServiceResult ValidationError(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary model)
        {
            return ValidationError(String.Join(" ", model.SelectMany(t => t.Value.Errors).Select(t => t.ErrorMessage).ToArray()));
        }

        public ServiceResult ValidationError(IEnumerable<string> validationMesssages)
        {
            return ValidationError(string.Join(" ", validationMesssages));
        }

        protected T ValidationError<T>(string validationMessage) where T : ServiceResult, new()
        {
            return new T()
            {
                StatusCode = HttpStatusCode.BadRequest,
                StatusDescription = validationMessage
            };
        }

        protected T ValidationError<T>(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary model) where T : ServiceResult, new()
        {
            return ValidationError<T>(String.Join(" ", model.SelectMany(t => t.Value.Errors).Select(t => t.ErrorMessage).ToArray()));
        }

        public T ValidationError<T>(IEnumerable<string> validationMesssages) where T : ServiceResult, new()
        {
            return ValidationError<T>(string.Join(" ", validationMesssages));
        }

        #endregion

        #endregion        
    }
}