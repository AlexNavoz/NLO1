#if UNITY_ANDROID || true
 using UnityEngine;
 using System;
 using System.Collections.Generic;
 using GooglePlayGames;
 using GooglePlayGames.BasicApi;
 using GooglePlayGames.BasicApi.SavedGame;
 using System.Text;
 using UnityEngine.SocialPlatforms;
 using UnityEngine.UI;
 
 public class GPG_CloudSaveSystem {
 
     private static GPG_CloudSaveSystem _instance;
     public static GPG_CloudSaveSystem Instance{
         get{
             if (_instance == null) {
                 _instance = new GPG_CloudSaveSystem();
             }
             return _instance;
         }
     }
 
     private bool m_saving;
     private static string m_saveName = "typicalufo_gamesave";
     private string saveString = "testsave";
 
     private bool Authenticated {
         get {
             return Social.Active.localUser.authenticated;
         }
     }

    public void Start() {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
         .EnableSavedGames()
         .Build();
        PlayGamesPlatform.InitializeInstance(config);
    }
 
     private void ProcessCloudData(byte[] cloudData) {
         if (cloudData == null) {
             Debug.Log("No data saved to the cloud yet...");
             return;
         }
         Debug.Log("Decoding cloud data from bytes.");
         string progress = FromBytes(cloudData);
         Debug.Log("Merging with existing game progress.");
         MergeWith(progress);
     }
 
     public void LoadFromCloud(){
         Debug.Log("Loading game progress from the cloud.");
         m_saving = false;
         ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
             m_saveName,
             DataSource.ReadCacheOrNetwork,
             ConflictResolutionStrategy.UseLongestPlaytime,
             SavedGameOpened);
     }
 
     public void SaveToCloud() {
         if (Authenticated) {
             Debug.Log("Saving progress to the cloud... filename: " + m_saveName);
             m_saving = true;
             ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                 m_saveName,
                 DataSource.ReadCacheOrNetwork,
                 ConflictResolutionStrategy.UseLongestPlaytime,
                 SavedGameOpened);
         } else {
             Debug.Log("Not authenticated!");
         }
     }
 
     private void SavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
         if (status == SavedGameRequestStatus.Success){
             if (m_saving){
                 byte[] data = ToBytes();
                 SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                 SavedGameMetadataUpdate updatedMetadata = builder.Build();
                 ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, SavedGameWritten);
             } else {
                 ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
             }
         } else {
             Debug.LogWarning("Error opening game: " + status);
         }
     }
 
     private void SavedGameLoaded(SavedGameRequestStatus status, byte[] data) {
         if (status == SavedGameRequestStatus.Success){
             Debug.Log("SaveGameLoaded, success=" + status);
             ProcessCloudData(data);
         } else {
             Debug.LogWarning("Error reading game: " + status);
         }
     }
 
     private void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game) {
         if (status == SavedGameRequestStatus.Success){
             Debug.Log("Game " + game.Description + " written");
         } else {
             Debug.LogWarning("Error saving game: " + status);
         }
     }
 
     private void MergeWith(string other) {
         if (other != "") {
             saveString = other;
         } else {
             Debug.Log("Loaded save string doesn't have any content");
         }
     }
 
     private byte[] ToBytes() {
         byte[] bytes = Encoding.UTF8.GetBytes(saveString);
         return bytes;
     }
 
     private string FromBytes(byte[] bytes) {
         string decodedString = Encoding.UTF8.GetString(bytes);
         return decodedString;
     } 
 }
#endif