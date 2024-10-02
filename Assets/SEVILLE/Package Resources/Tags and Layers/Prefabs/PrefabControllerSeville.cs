using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.IO;

[InitializeOnLoad]
public class PrefabControllerSeville
{
    private static string serverURL = "https://trial.svcc.io/api/check-trial";
    private static string deviceID;
    private static IEnumerator coroutine;
    private static string scriptsFolderPath = "Assets/SEVILLE/Package Resources/Scripts";
    private static string pluginsFolderPath = "Assets/SEVILLE/Plugins";
    private static string trialStatusKey = "Seville_TrialStatus"; // Key untuk menyimpan status trial di EditorPrefs

    // Static constructor untuk dipanggil ketika Editor Unity dijalankan
    static PrefabControllerSeville()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log("Device ID: " + deviceID);

        // Mulai proses untuk mengirim deviceID dan memulai pengecekan
        EditorApplication.update += RunCoroutine;
        coroutine = CheckTrialStatus();

        // Tambahkan pengecekan ketika editor dibuka kembali
        CheckAndShowWindowOnEditorOpen();
    }

    // Fungsi untuk memulai coroutine
    private static void RunCoroutine()
    {
        if (coroutine != null && !coroutine.MoveNext())
        {
            // Hentikan update setelah coroutine selesai
            EditorApplication.update -= RunCoroutine;
        }
    }

    // Fungsi yang dipanggil saat Editor dibuka kembali
    private static void CheckAndShowWindowOnEditorOpen()
    {
        // Cek status trial dari EditorPrefs
        string savedTrialStatus = EditorPrefs.GetString(trialStatusKey, "active"); // Default ke 'active'
        if (savedTrialStatus == "expired")
        {
            SevilleInfoWindow.ShowWindow(); // Jika status expired, tampilkan window
        }
    }

    // Fungsi untuk mengecek status trial dengan API
    private static IEnumerator CheckTrialStatus()
    {
        string jsonPayload = JsonUtility.ToJson(new DeviceInfo { deviceId = deviceID });
        Debug.Log("JSON Payload: " + jsonPayload);

        using (UnityWebRequest www = new UnityWebRequest(serverURL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending request: " + www.error);
            }
            else
            {
                Debug.Log("Response from server: " + www.downloadHandler.text);

                string jsonResponse = www.downloadHandler.text;
                TrialApiResponse response = null;

                try
                {
                    response = JsonUtility.FromJson<TrialApiResponse>(jsonResponse);
                    Debug.Log("Parsed Response - status: " + response.status + ", endDate: " + response.trial.endDate);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error parsing response: " + e.Message);
                }

                // Tampilkan pop-up sesuai status trial
                if (response != null && response.status == "active")
                {
                    Debug.Log("Trial is valid. Plugin remains active.");
                    // Simpan status active ke EditorPrefs
                    EditorPrefs.SetString(trialStatusKey, "active");
                }
                else if (response != null && response.status == "expired")
                {
                    if (!string.IsNullOrEmpty(response.trial.endDate))
                    {
                        DateTime trialEndDate;
                        if (DateTime.TryParse(response.trial.endDate, out trialEndDate))
                        {
                            DateTime trialStartDate;
                            if (DateTime.TryParse(response.trial.startDate, out trialStartDate))
                            {
                                DateTime calculatedEndDate = trialStartDate.AddDays(10); // Tambahkan 7 hari dari startDate

                                if (System.DateTime.Now > calculatedEndDate)
                                {
                                    Debug.LogWarning("Trial expired. Disabling plugin and deleting scripts and plugins.");

                                    // Tampilkan window ketika trial habis
                                    SevilleInfoWindow.ShowWindow();  // Tampilkan informasi seputar Seville ketika expired
                                    EditorPrefs.SetString(trialStatusKey, "expired"); // Simpan status expired

                                    DeleteScriptsAndPlugins();
                                }
                                else
                                {
                                    Debug.Log("Trial masih dalam masa 10 hari, plugin tetap aktif.");
                                }
                            }
                            else
                            {
                                Debug.LogError("Invalid startDate format: " + response.trial.startDate);
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid endDate format: " + response.trial.endDate);
                        }
                    }
                    else
                    {
                        Debug.LogError("endDate is null or empty in the response.");
                    }
                }
                else
                {
                    Debug.LogWarning("Trial status unclear or trial restarted. No deletion.");
                }
            }
        }
    }

    // Fungsi untuk menghapus folder Scripts dan Plugins
    private static void DeleteScriptsAndPlugins()
    {
        Debug.Log("Deleting Scripts and Plugins folders...");

        // Hapus folder Scripts
        if (Directory.Exists(scriptsFolderPath))
        {
            Debug.Log("Deleting Scripts folder: " + scriptsFolderPath);
            Directory.Delete(scriptsFolderPath, true);  // Hapus folder Scripts beserta isinya
        }
        else
        {
            Debug.LogError("Scripts folder not found!");
        }

        // Hapus folder Plugins
        if (Directory.Exists(pluginsFolderPath))
        {
            Debug.Log("Deleting Plugins folder: " + pluginsFolderPath);
            Directory.Delete(pluginsFolderPath, true);  // Hapus folder Plugins beserta isinya
        }
        else
        {
            Debug.LogError("Plugins folder not found!");
        }
    }
}

// Kelas untuk serialisasi JSON
[System.Serializable]
public class DeviceInfo
{
    public string deviceId;
}

[System.Serializable]
public class Trial
{
    public int id;
    public string deviceId;
    public string startDate;
    public string endDate;
}

[System.Serializable]
public class TrialApiResponse
{
    public string message;
    public string status;
    public Trial trial;
}
