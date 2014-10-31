using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;

namespace Nop.Services.Directory
{
    /// <summary>
    /// State province service
    /// </summary>
    public partial class StateProvinceService : IStateProvinceService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {1} : country ID
        /// </remarks>
        private const string STATEPROVINCES_ALL_KEY = "Nop.stateprovince.all-{0}";
        /// <summary>
        /// {1} : StateProvince ID
        /// </summary>
        private const string CITIES_ALL_KEY = "Nop.city.all-{0}";
        /// <summary>
        /// {1} : City ID
        /// </summary>
        private const string AREA_ALL_KEY = "Nop.area.all-{0}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string STATEPROVINCES_PATTERN_KEY = "Nop.stateprovince.";

        #endregion

        #region Fields

        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Area> _areaRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="stateProvinceRepository">State/province repository</param>
        /// <param name="eventPublisher">Event published</param>
        public StateProvinceService(ICacheManager cacheManager,
            IRepository<StateProvince> stateProvinceRepository,
            IRepository<City> cityRepository,
            IRepository<Area> areaRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _stateProvinceRepository = stateProvinceRepository;
            _cityRepository = cityRepository;
            _areaRepository = areaRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Deletes a state/province
        /// </summary>
        /// <param name="stateProvince">The state/province</param>
        public virtual void DeleteStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Delete(stateProvince);

            _cacheManager.RemoveByPattern(STATEPROVINCES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(stateProvince);
        }

        /// <summary>
        /// Gets a state/province
        /// </summary>
        /// <param name="stateProvinceId">The state/province identifier</param>
        /// <returns>State/province</returns>
        public virtual StateProvince GetStateProvinceById(int stateProvinceId)
        {
            if (stateProvinceId == 0)
                return null;

            return _stateProvinceRepository.GetById(stateProvinceId);
        }

        /// <summary>
        /// Gets a state/province 
        /// </summary>
        /// <param name="abbreviation">The state/province abbreviation</param>
        /// <returns>State/province</returns>
        public virtual StateProvince GetStateProvinceByAbbreviation(string abbreviation)
        {
            var query = from sp in _stateProvinceRepository.Table
                        where sp.Abbreviation == abbreviation
                        select sp;
            var stateProvince = query.FirstOrDefault();
            return stateProvince;
        }

        /// <summary>
        /// Gets a state/province collection by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>State/province collection</returns>
        public virtual IList<StateProvince> GetStateProvincesByCountryId(int countryId, bool showHidden = false)
        {
            string key = string.Format(STATEPROVINCES_ALL_KEY, countryId);
            return _cacheManager.Get(key, () =>
            {
                var query = from sp in _stateProvinceRepository.Table
                            orderby sp.DisplayOrder
                            where sp.CountryId == countryId &&
                            (showHidden || sp.Published)
                            select sp;
                var stateProvinces = query.ToList();
                return stateProvinces;
            });
        }

        /// <summary>
        /// Inserts a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        public virtual void InsertStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Insert(stateProvince);

            _cacheManager.RemoveByPattern(STATEPROVINCES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(stateProvince);
        }

        /// <summary>
        /// Updates a state/province
        /// </summary>
        /// <param name="stateProvince">State/province</param>
        public virtual void UpdateStateProvince(StateProvince stateProvince)
        {
            if (stateProvince == null)
                throw new ArgumentNullException("stateProvince");

            _stateProvinceRepository.Update(stateProvince);

            _cacheManager.RemoveByPattern(STATEPROVINCES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(stateProvince);
        }

        #endregion


        public IList<City> GetCitiesByStateProvinceId(int stateProvinceId, bool showHidden = false)
        {
            string key = string.Format(CITIES_ALL_KEY, stateProvinceId);
            return _cacheManager.Get(key, () =>
            {
                var query = from sp in _cityRepository.Table
                            orderby sp.DisplayOrder
                            where sp.ProvinceId == stateProvinceId &&
                            (showHidden || sp.Published)
                            select sp;
                var stateProvinces = query.ToList();
                return stateProvinces;
            });
        }

        public IList<Area> GetAreasByCityId(int cityId, bool showHidden = false)
        {
            string key = string.Format(AREA_ALL_KEY, cityId);
            return _cacheManager.Get(key, () =>
            {
                var query = from sp in _areaRepository.Table
                            orderby sp.DisplayOrder
                            where sp.CityId == cityId &&
                            (showHidden || sp.Published)
                            select sp;
                var areas = query.ToList();
                return areas;
            });
        }

        public City GetCityById(int cityId)
        {
            if (cityId == 0)
                return null;

            return _cityRepository.GetById(cityId);
        }
        public Area GetAreaById(int areaId)
        {
            if (areaId == 0)
                return null;
            return _areaRepository.GetById(areaId);
        }
    }
}
