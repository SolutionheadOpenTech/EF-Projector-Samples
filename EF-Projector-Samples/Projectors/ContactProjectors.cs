using System;
using System.Linq.Expressions;
using EFSplitProjectorTests.Models;
using EFSplitProjectorTests.Projectors.ReturnModels;

namespace EFSplitProjectorTests.Projectors
{
    public static class ContactProjectors
    {
        public static Expression<Func<Contact, ContactReturn>> SelectContact()
        {
            return c => new ContactReturn
                {
                    Title = c.Title,
                    Suffix = c.Suffix,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName
                };
        }
    }
}