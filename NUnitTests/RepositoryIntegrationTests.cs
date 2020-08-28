using DatingApp.API.Data;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTests
{
    public class RepositoryIntegrationTests
    {
        private DatingRepository _datingRepository;
        private DataContext _dataContext;

        [SetUp]
        public void SetUp()
        {
            var options =
                new DbContextOptionsBuilder<DataContext>().UseSqlite("DataSource=datingapp.db", x => { })
                    .Options;

            // new instance of ElectronicsContext will be created for every test.
            _dataContext = new DataContext(options);
            _dataContext.Database.OpenConnection();
            _dataContext.Database.EnsureCreated();
            
            // Use _context to insert initial data required for the test
            //Seed.SeedUsers(_dataContext);
            _datingRepository = new DatingRepository(_dataContext);

        }

        [TearDown]
        public void TearDown()
        {
            _dataContext.Dispose();
        }

        [Test]
        public async Task GetUsers_Success()
        {

            UserParams userParams = new UserParams
            {
                UserId = _dataContext.Users.First(x => x.Gender == "female").Id,
                PageNumber = 1,
                PageSize = 5,
                Gender = "female"
            };

            var result = await _datingRepository.GetUsers(userParams);

            Assert.IsNotNull(result);
            // check that the users returned do not include the current user
            Assert.AreEqual(_dataContext.Users.Count(x => x.Gender == userParams.Gender) - 1, result.TotalCount);
            Assert.IsNull(result.FirstOrDefault(x => x.Id == userParams.UserId));
        }

        [Test]
        public async Task GetPhoto_Success()
        {
            var photo = await _dataContext.Photos.FirstAsync();
            var result = await _datingRepository.GetPhoto(photo.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(photo.Id, result.Id);
        }

        [Test]
        public async Task GetUser_Success()
        {
            var user = await _dataContext.Users.FirstAsync();
            var result = await _datingRepository.GetUser(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [Test]
        public async Task GetMainPhotoForUser_Success()
        {
            //arrange
            var user = await _dataContext.Users.FirstAsync(x => x.Photos.Any());
            //run
            var result = await _datingRepository.GetMainPhotoForUser(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Photos.FirstOrDefault(x => x.IsMain == true).Id, result.Id);
        }


        [Test]
        public async Task GetLike_Success()
        {
            //arrange
            var liker = await _dataContext.Users.FirstAsync();
            var likee = await _dataContext.Users.FirstAsync(x => x.Id != liker.Id);
            var likeModel = new Like
            {
                LikeeId = likee.Id,
                LikerId = liker.Id
            };

            await _dataContext.Likes.AddAsync(likeModel);
            await _dataContext.SaveChangesAsync();

            //run
            var result = await _datingRepository.GetLike(liker.Id, likee.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(liker, result.Liker);
            Assert.AreEqual(likee, result.Likee);

        }


        [Test]
        public async Task GetMessage_Success()
        {
            //arrange
            var user = await _dataContext.Users.FirstAsync();
            var recipient = await _dataContext.Users.FirstAsync(x => x.Id != user.Id);
            var messageModel = new Message
            {
                SenderId = user.Id,
                MessageSent = DateTime.Now,
                RecipientId = recipient.Id,
                IsRead = false,
                Content = "Hello World"
            };

            await _dataContext.Messages.AddAsync(messageModel);
            var messageId = await _dataContext.SaveChangesAsync();

            //run
            var result = await _datingRepository.GetMessage(messageId);
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result.Sender);
            Assert.AreEqual(recipient, result.Recipient);

        }

        [Test]
        public async Task GetMessageForUser_Success()
        {
            //arrange
            var user = await _dataContext.Users.FirstAsync();
            var recipient = await _dataContext.Users.FirstAsync(x => x.Id != user.Id);
            var messageModel = new Message
            {
                SenderId = user.Id,
                MessageSent = DateTime.Now,
                RecipientId = recipient.Id,
                IsRead = true,
                Content = "Hello World",
                DateRead = DateTime.Now,
                SenderDeleted = false,
                RecipientDeleted = false
            };

            await _dataContext.Messages.AddAsync(messageModel);

            var messageModel2 = new Message
            {
                SenderId = recipient.Id,
                MessageSent = DateTime.Now,
                RecipientId = user.Id,
                IsRead = false,
                Content = "Hello you!"
            };

            await _dataContext.Messages.AddAsync(messageModel);
            await _dataContext.SaveChangesAsync();

            var messageParams = new MessageParams
            {
                PageNumber = 1,
                PageSize = 5,
                UserId = user.Id,
                MessageContainer = "Outbox"
            };

            //run
            var result = await _datingRepository.GetMessagesForUser(messageParams);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

        }

    }
}