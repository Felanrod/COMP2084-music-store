using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvc_music_store.Controllers;
using Moq;
using mvc_music_store.Models;
using System.Collections.Generic;
using System.Linq;

namespace mvc_music_store.Tests.Controllers
{
    [TestClass]
    public class AlbumsControllerTest
    {
        // global variables
        AlbumsController controller;
        Mock<IAlbumsMock> mock;
        List<Album> albums;

        [TestInitialize]

        public void TestInitialize()
        {
            // this method runs automatically before each individual test

            // create a new mock data object to hold a fake list of albums
            mock = new Mock<IAlbumsMock>();

            // populate mock list
            albums = new List<Album>
            {
                new Album { AlbumId = 100, Title = "One Hundred", Price = 6.99m, Artist = new Artist {
                    ArtistId = 1000, Name = "Singer One"
                    }
                },

                new Album { AlbumId = 300, Title = "Three Hundred", Price = 8.99m, Artist = new Artist {
                    ArtistId = 1001, Name = "Singer Two"
                    }
                },

                new Album { AlbumId = 200, Title = "Two Hundred", Price = 7.99m, Artist = new Artist {
                    ArtistId = 1001, Name = "Singer Two"
                    }
                }
            };

            // put list into mock object and pass it to the albums controller
            // albums.OrderBy(a => a.Artist.Name).ThenBy(a => a.Title);

            mock.Setup(m => m.Albums).Returns(albums.AsQueryable());
            controller = new AlbumsController(mock.Object);
        }

        [TestMethod]
        public void IndexLoadsView()
        {
            // arrange
            // AlbumsController controller = new AlbumsController();

            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsAlbums()
        {
            // arrange

            // act
            var result = (List<Album>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(albums, result);
        }
    }
}
