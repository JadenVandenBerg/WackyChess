using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Firebase SDKs
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;

public class UnityFirestoreAccount : MonoBehaviour
{
    public FirebaseFirestore Firestore { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseApp FirebaseAppInstance { get; private set; }

    public PlayerProfile LocalProfile { get; private set; }

    [Serializable]
    [FirestoreData]
    public class PlayerProfile
    {
        [FirestoreProperty] public string uid { get; set; }
        [FirestoreProperty] public string displayName { get; set; }
    }

    async void Start()
    {
        try
        {
            // --- Initialize Firebase ---
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {dependencyStatus}");
                return;
            }

            FirebaseAppInstance = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            Firestore = FirebaseFirestore.DefaultInstance;

            // --- Anonymous Firebase Sign-In ---
            var authResult = await Auth.SignInAnonymouslyAsync();
            var user = authResult.User;
            Debug.Log($"[FirebaseAuth] Signed in anonymously as UID: {user.UserId}");

            // --- Load or Create Player Profile ---
            await LoadPlayerProfile(user.UserId);

            await AddToInventory(user.UserId, "Amazon");
        }
        catch (Exception e)
        {
            Debug.LogError($"[UnityFirestoreAccount] Initialization failed: {e}");
        }
    }

    public async Task SavePlayerProfile(PlayerProfile profile)
    {
        if (Firestore == null)
            throw new Exception("Firestore not initialized");

        await Firestore.Collection("players").Document(profile.uid).SetAsync(profile);
        LocalProfile = profile;
        Debug.Log($"[UnityFirestoreAccount] Saved profile for {profile.uid}");
    }

    public async Task LoadPlayerProfile(string uid)
    {
        if (Firestore == null)
            throw new Exception("Firestore not initialized");

        var docRef = Firestore.Collection("players").Document(uid);
        var snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            LocalProfile = snapshot.ConvertTo<PlayerProfile>();
            Debug.Log($"[UnityFirestoreAccount] Loaded existing profile for {uid}");
        }
        else
        {
            LocalProfile = new PlayerProfile
            {
                uid = uid,
                displayName = "Player"
            };

            await SavePlayerProfile(LocalProfile);
            Debug.Log($"[UnityFirestoreAccount] Created new profile for {uid}");
        }
    }

    public async Task AddToInventory(string uid, string itemToAdd)
    {
        if (Firestore == null)
            throw new Exception("Firestore not initialized");

        var docRef = Firestore.Collection("players").Document(uid);

        await docRef.UpdateAsync("Inventory", FieldValue.ArrayUnion(itemToAdd));
    }
}
