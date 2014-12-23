using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EF_Split_Projector;
using EF_Split_Projector.Helpers.Extensions;
using NUnit.Framework;

namespace EF_Projector_Samples.Tests
{
    [TestFixture]
    public abstract class QueryTestsBase<TContext, TSource, TDest> : TestsBase
        where TContext : class, new()
        where TDest : new()
    {
        protected TContext Context { get { return _context ?? (_context = new TContext()); } }
        private TContext _context;

        protected abstract IQueryable<TSource> GetSourceQuery();
        protected abstract IEnumerable<Expression<Func<TSource, TDest>>> GetProjectors();
        protected virtual void Post(IEnumerable<TDest> results) { }

        private void RunTest(IQueryable<TDest> query)
        {
            StartWatch();
            var results = query.ToList();
            StopWatchWriteTime();

            Post(results);

            Console.WriteLine("Records: {0}", results.Count);
        }

        [Test]
        public void SelectTest()
        {
            _context = null;
            RunWarmup(this);

            var query = GetSourceQuery().Select(GetProjector());
            RunTest(query);
            QueryString(query.ToString());
        }

        [Test]
        public void SplitSelectTest()
        {
            _context = null;
            RunWarmup(this);

            var query = GetSourceQuery().SplitSelect(GetProjectors());
            RunTest(query);
            QueryString(((SplitQueryableBase)query).CommandString);
        }

        private Expression<Func<TSource, TDest>> GetProjector()
        {
            return GetProjectors().Aggregate((Expression<Func<TSource, TDest>>)null, (c, s) => c == null ? s : c.Merge(s));
        }

        private static void QueryString(string query)
        {
            Console.WriteLine("QueryCharacters: {0} Lines: {1}", query.Count(), query.Split('\n').Count());
            Console.WriteLine(query);
        }

        private static bool _ranWarmup;

        private static void RunWarmup(QueryTestsBase<TContext, TSource, TDest> queryTestsBase)
        {
            if(!_ranWarmup)
            {
                _ranWarmup = true;
                queryTestsBase.StartWatch();
                Assert.IsTrue(queryTestsBase.GetSourceQuery().Select(queryTestsBase.GetProjector()).ToList().Count > -1);
                queryTestsBase.StopWatchWriteTime("Warmup");
            }
            else
            {
                Console.WriteLine("Warmed up");
            }
        }
    }
}