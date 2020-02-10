using Game.Models;
using Game.Views;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using Game.Services;

namespace Game.ViewModels
{

    public class CharIndexViewModel : BaseViewModel<CharacterModel>
    {
        #region Singleton

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static volatile CharIndexViewModel instance;
        private static readonly object syncRoot = new Object();

        public static CharIndexViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CharIndexViewModel();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion Singleton

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// The constructor subscribes message listeners for crudi operations
        /// </summary>
        public CharIndexViewModel()
        {
            Title = "Characters";

            //#region Messages

            //// Register the Create Message
            //MessagingCenter.Subscribe<CharacterCreatePage, CharacterModel>(this, "Create", async (obj, data) =>
            //{
            //    await CreateAsync(data as CharacterModel);
            //});

            //// Register the Update Message
            //MessagingCenter.Subscribe<CharacterUpdatePage, CharacterModel>(this, "Update", async (obj, data) =>
            //{
            //    // Have the item update itself
            //    data.Update(data);

            //    await UpdateAsync(data as CharacterModel);
            //});

            //// Register the Delete Message
            //MessagingCenter.Subscribe<CharacterDeletePage, CharacterModel>(this, "Delete", async (obj, data) =>
            //{
            //    await DeleteAsync(data as CharacterModel);
            //});


        }

        #endregion Constructor
    }
}