using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WMC.Core.DataProvider2008
{
	/// <summary>
	/// Summary description for CacheDataProvider.
	/// </summary>
	public class CacheDataProvider : IDataProvider 
	{

		//  static variables to denote the cache.  
		//  NOTE:  operating this class in a cluster will require more work to synchronize the hashes 
		//	across the cluster.
		private static Hashtable cache = null;
		private static Hashtable synchronizedCache = null;

		//  stubs for IDataProvider implementations.  Note they are virtual.
		public virtual void StartTransaction() {}
		public virtual void CommitTransaction() {}
		public virtual void RollbackTransaction() {}
		
		


		private static void InitCache() 
		{
			System.Diagnostics.Debug.WriteLine("CacheDataProvider is initializing cache...");
			cache = new Hashtable();
			synchronizedCache = Hashtable.Synchronized(cache);
		}

		protected void RemoveFromCache(object key) 
		{
			System.Diagnostics.Debug.WriteLine("CacheDataProvider is removing from cache key='" + key + "'");
			synchronizedCache.Remove(key);
		}
		
		protected void EmptyCache() 
		{
			synchronizedCache.Clear();
		}
		
		protected void AddToCache(object key, object val) 
		{
			System.Diagnostics.Debug.WriteLine("CacheDataProvider is adding to cache key='" + key + "'");
			synchronizedCache[key] = val;
		}

		protected object GetFromCache(object key) 
		{
			System.Diagnostics.Debug.WriteLine("CacheDataProvider cache lookup for key='" + key + "' returning: " + synchronizedCache[key]);
			return synchronizedCache[key];
		}

		

		// constructor
		public CacheDataProvider() 
		{
			if (CacheDataProvider.cache == null) 
			{
				CacheDataProvider.InitCache();
			}
		}

		//
		//	The following are static functions that can be called directly.
		//	These should only be used by tools that manage the cache directly...
		//	
		public static Hashtable GetCompleteCache() 
		{
			if (cache == null) 
			{
				InitCache();
			}
			return synchronizedCache;
		}

		public static void ClearCache() 
		{
			if (cache == null) 
			{
				InitCache();
			}
			synchronizedCache.Clear();
		}

		public static void RemoveItem(string key) 
		{
			if (cache == null) 
			{
				InitCache();
			}
			synchronizedCache.Remove(key);
		}
	}
}
