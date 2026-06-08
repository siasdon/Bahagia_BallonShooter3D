# 🎈 Tutorial: Game Tembak Balon di Unity
## Panduan Pemula — Core Loop Lengkap

---

## Apa yang Akan Dibuat

Game tembak balon 3D sederhana dengan alur penuh:
- Balon muncul acak dari bawah layar, bergerak naik + berayun
- Pemain **klik** balon untuk menembak (raycast)
- Tiap warna memberi poin berbeda
- Timer 60 detik hitung mundur → habis = **Game Over**
- Skor cukup → **Level naik**, balon makin cepat muncul
- Bisa **Retry** atau kembali ke **Main Menu**

**Poin per Warna (sesuai diagram):**

| Merah | Biru | Hijau | Emas |
|-------|------|-------|------|
| 10    | 20   | 30    | 50   |

---

## File yang Akan Dibuat

```
Assets/
├── Scripts/
│   ├── GameManager.cs       ← Skor, timer, state game
│   ├── UIManager.cs         ← Update tampilan layar
│   ├── BalloonSpawner.cs    ← Kemunculan balon
│   ├── Balloon.cs           ← Gerakan & hit balon
│   ├── PlayerShooter.cs     ← Tembak dengan klik
│   └── MainMenuController.cs← Tombol main menu
├── Prefabs/
│   ├── BalloonRed.prefab
│   ├── BalloonBlue.prefab
│   ├── BalloonGreen.prefab
│   └── BalloonGold.prefab
├── Materials/
│   └── (4 material warna)
└── Scenes/
    ├── MainMenu.unity
    └── GameScene.unity
```

---

## LANGKAH 1 — Buat Project Baru

1. Buka **Unity Hub** → klik **New Project**
2. Pilih template: **3D (Core)**
3. Nama project: `GameTembakBalon`
4. Klik **Create Project** dan tunggu sampai selesai

---

## LANGKAH 2 — Install TextMeshPro & Buat Folder

### Install TextMeshPro (wajib untuk teks UI)

1. Menu Unity → **Window > Package Manager**
2. Di dropdown kiri atas pilih: **Unity Registry**
3. Cari `TextMeshPro` → klik **Install**
4. Jika muncul popup → klik **Import TMP Essentials**

### Buat Folder di Panel Project

Klik kanan folder `Assets` → **Create > Folder** — buat 4 folder:

- `Scripts`
- `Prefabs`
- `Materials`
- `Scenes`

---

## LANGKAH 3 — Buat 2 Scene

### Scene 1: MainMenu

1. **File > New Scene** → pilih **Basic (Built-in)** → Create
2. **File > Save As** → simpan sebagai `MainMenu` di folder `Assets/Scenes`

### Scene 2: GameScene

1. **File > New Scene** → pilih **Basic (Built-in)** → Create
2. **File > Save As** → simpan sebagai `GameScene` di folder `Assets/Scenes`

### Daftarkan ke Build Settings

1. **File > Build Settings**
2. Buka scene `MainMenu` → klik **Add Open Scenes**
3. Buka scene `GameScene` → klik **Add Open Scenes**
4. ⚠️ Pastikan `MainMenu` = **Index 0** dan `GameScene` = **Index 1**

---

## LANGKAH 4 — Buat Layer "Balloon"

> Layer ini penting agar tembakan hanya mengenai balon, bukan objek lain.

1. Menu atas → **Edit > Project Settings > Tags and Layers**
2. Cari bagian **Layers** → expand
3. Pada baris **User Layer 6** (atau baris kosong manapun), ketik: `Balloon`
4. Tutup Project Settings

---

## LANGKAH 5 — Buat 4 Material Balon

Di folder `Assets/Materials`:

1. Klik kanan → **Create > Material** — buat 4 material:

| Nama File | Warna Albedo |
|-----------|-------------|
| `MatMerah` | Merah `(255, 50, 50)` |
| `MatBiru` | Biru `(50, 100, 255)` |
| `MatHijau` | Hijau `(50, 200, 50)` |
| `MatEmas` | Kuning/Emas `(255, 200, 0)` |

2. Untuk setiap material: klik material → klik kotak warna di **Albedo** → pilih warna

---

## LANGKAH 6 — Tulis 6 Script

Klik kanan folder `Assets/Scripts` → **Create > C# Script** — buat file bernama persis seperti di bawah.

> ⚠️ **Penting:** Nama file harus SAMA PERSIS dengan nama class!

---

### SCRIPT 1: `GameManager.cs`

> Mengatur skor, timer, level, dan state game (bermain / game over).

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton: bisa diakses dari script manapun lewat GameManager.Instance
    public static GameManager Instance { get; private set; }

    [Header("Pengaturan Game")]
    public float gameDuration = 60f;    // Durasi game (detik)
    public int levelUpTarget = 100;     // Skor untuk naik level

    private float timeRemaining;
    private int score;
    private int level = 1;
    private bool isPlaying;

    void Awake()
    {
        // Pastikan hanya ada 1 GameManager di scene
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        timeRemaining = gameDuration;
        score = 0;
        level = 1;
        isPlaying = true;
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateTimer(Mathf.CeilToInt(timeRemaining));
        UIManager.Instance.HideGameOver();
    }

    void Update()
    {
        if (!isPlaying) return;

        // Hitung mundur timer setiap frame
        timeRemaining -= Time.deltaTime;
        UIManager.Instance.UpdateTimer(Mathf.CeilToInt(timeRemaining));

        // Waktu habis = Game Over
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UIManager.Instance.UpdateScore(score);

        // Cek apakah skor cukup untuk naik level
        if (score >= levelUpTarget * level)
            LevelUp();
    }

    void LevelUp()
    {
        level++;
        BalloonSpawner.Instance.IncreaseSpeed();   // Percepat spawn balon
        UIManager.Instance.ShowLevelUp(level);
    }

    public void EndGame()
    {
        isPlaying = false;
        BalloonSpawner.Instance.StopSpawning();
        UIManager.Instance.ShowGameOver(score);
    }

    // Dipakai oleh script Balloon & PlayerShooter untuk cek apakah game aktif
    public bool IsPlaying() => isPlaying;

    // Dipanggil oleh tombol "Ulangi" di UI
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Dipanggil oleh tombol "Menu Utama" di UI
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
```

---

### SCRIPT 2: `UIManager.cs`

> Mengupdate semua elemen tampilan: skor, timer, panel game over, level up.

```csharp
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Teks HUD")]
    public TextMeshProUGUI scoreText;       // Teks skor di layar
    public TextMeshProUGUI timerText;       // Teks waktu di layar

    [Header("Panel Game Over")]
    public GameObject gameOverPanel;         // Panel yang muncul saat game selesai
    public TextMeshProUGUI finalScoreText;   // Teks skor akhir di panel

    [Header("Panel Level Up")]
    public GameObject levelUpPanel;
    public TextMeshProUGUI levelUpText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateScore(int value)
    {
        if (scoreText) scoreText.text = "Skor: " + value;
    }

    public void UpdateTimer(int seconds)
    {
        if (timerText) timerText.text = "Waktu: " + seconds + "s";
    }

    public void ShowGameOver(int finalScore)
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        if (finalScoreText) finalScoreText.text = "Skor Akhir: " + finalScore;
    }

    public void HideGameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    public void ShowLevelUp(int newLevel)
    {
        if (!levelUpPanel) return;
        levelUpPanel.SetActive(true);
        if (levelUpText) levelUpText.text = "LEVEL " + newLevel + "!";
        Invoke(nameof(HideLevelUp), 2f);  // Auto-hilang setelah 2 detik
    }

    void HideLevelUp()
    {
        if (levelUpPanel) levelUpPanel.SetActive(false);
    }
}
```

---

### SCRIPT 3: `BalloonSpawner.cs`

> Mengelola kemunculan balon secara terus-menerus dengan jeda, dan
> memilih warna balon secara acak berdasarkan peluang yang berbeda.

```csharp
using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour
{
    public static BalloonSpawner Instance { get; private set; }

    [Header("Prefab Balon — drag dari folder Prefabs")]
    public GameObject balloonRedPrefab;
    public GameObject balloonBluePrefab;
    public GameObject balloonGreenPrefab;
    public GameObject balloonGoldPrefab;

    [Header("Pengaturan Spawn")]
    public float spawnInterval = 2f;       // Jeda antar spawn (detik)
    public float minSpawnInterval = 0.5f;  // Jeda minimum saat level tinggi
    public float spawnXRange = 4f;         // Lebar area spawn (kiri-kanan)
    public float spawnY = -6f;             // Posisi Y saat spawn (bawah layar)

    private Coroutine spawnRoutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (GameManager.Instance != null && GameManager.Instance.IsPlaying())
                SpawnOneBalloon();
        }
    }

    void SpawnOneBalloon()
    {
        GameObject prefab = PickBalloon();
        if (prefab == null) return;

        // Posisi X acak, Y di bawah layar
        float x = Random.Range(-spawnXRange, spawnXRange);
        Instantiate(prefab, new Vector3(x, spawnY, 0f), Quaternion.identity);
    }

    // Pilih jenis balon: Merah paling sering, Emas paling jarang
    GameObject PickBalloon()
    {
        int roll = Random.Range(1, 101);          // Angka acak 1-100
        if (roll <= 50) return balloonRedPrefab;  // 50% = Merah (poin 10)
        if (roll <= 75) return balloonBluePrefab; // 25% = Biru (poin 20)
        if (roll <= 90) return balloonGreenPrefab;// 15% = Hijau (poin 30)
        return balloonGoldPrefab;                 // 10% = Emas (poin 50)
    }

    // Dipanggil GameManager saat level naik
    public void IncreaseSpeed()
    {
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.3f);
    }
}
```

---

### SCRIPT 4: `Balloon.cs`

> Mengontrol gerakan balon (naik + berayun seperti arus laut) dan
> apa yang terjadi saat balon ditembak.

```csharp
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public enum BalloonType { Red, Blue, Green, Gold }

    [Header("Tipe Balon")]
    public BalloonType balloonType = BalloonType.Red;

    [Header("Gerakan")]
    public float riseSpeed = 2f;     // Kecepatan naik ke atas
    public float swayAmount = 0.5f;  // Jarak ayunan kiri-kanan
    public float swaySpeed = 1.5f;   // Kecepatan ayunan
    public float destroyY = 7f;      // Hapus balon jika melewati batas ini

    private int pointValue;
    private float startX;      // Posisi X saat spawn (untuk perhitungan ayunan)
    private float timeOffset;  // Fase acak agar tiap balon tidak bergerak sync

    void Start()
    {
        startX = transform.position.x;
        timeOffset = Random.Range(0f, Mathf.PI * 2f);

        // Tentukan poin berdasarkan tipe balon
        switch (balloonType)
        {
            case BalloonType.Red:   pointValue = 10; break;
            case BalloonType.Blue:  pointValue = 20; break;
            case BalloonType.Green: pointValue = 30; break;
            case BalloonType.Gold:  pointValue = 50; break;
        }
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying()) return;

        // 1. Naik ke atas
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // 2. Ayun kiri-kanan (simulasi arus laut/angin)
        float xOffset = Mathf.Sin((Time.time + timeOffset) * swaySpeed) * swayAmount;
        transform.position = new Vector3(startX + xOffset, transform.position.y, transform.position.z);

        // 3. Hapus jika sudah melewati batas atas layar
        if (transform.position.y > destroyY)
            Destroy(gameObject);
    }

    // Dipanggil PlayerShooter saat balon kena tembak
    public void OnHit()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(pointValue);
        Destroy(gameObject);  // Balon pecah / hilang
    }
}
```

---

### SCRIPT 5: `PlayerShooter.cs`

> Mendeteksi klik mouse lalu menembak lewat raycast ke arah kursor.
> Jika kena balon → proses hit. Jika meleset → tidak ada poin.

```csharp
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Setup")]
    public Camera mainCamera;        // Main Camera dari Hierarchy
    public LayerMask balloonLayer;   // Centang layer "Balloon" di Inspector

    void Start()
    {
        // Jika camera tidak di-assign manual, cari otomatis
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying()) return;

        // Klik kiri mouse = tembak
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        // Buat "sinar" dari kamera ke posisi kursor di layar
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Cek apakah sinar mengenai objek dengan layer "Balloon"
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, balloonLayer))
        {
            Balloon balloon = hit.collider.GetComponent<Balloon>();
            if (balloon != null)
                balloon.OnHit();  // Kena! Balon akan hapus dirinya + tambah skor
        }
        // Jika meleset → tidak ada poin (sesuai diagram alur)
    }
}
```

---

### SCRIPT 6: `MainMenuController.cs`

> Mengontrol tombol di layar Main Menu.

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Dipanggil tombol "MULAI"
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Dipanggil tombol "KELUAR"
    public void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        // Hanya untuk editor Unity, bukan build final
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
```

---

## LANGKAH 7 — Buat Prefab Balon

Buka scene **GameScene**.

### Cara Buat 1 Prefab (BalloonRed) — Ulangi untuk 3 Lainnya

1. **GameObject > 3D Object > Sphere**
2. Rename: `BalloonRed`
3. Di Inspector atur:
   - **Layer** (dropdown atas Inspector): pilih `Balloon`
   - **Scale**: X=0.8, Y=1, Z=0.8 (sedikit oval seperti balon)
4. Drag material `MatMerah` dari folder Materials ke sphere
5. Klik **Add Component** → ketik `Balloon` → pilih script **Balloon**
6. Di component Balloon → set **Balloon Type** = `Red`
7. ✅ Pastikan ada **Sphere Collider** (biasanya sudah ada otomatis)
8. ❌ Jika ada **Rigidbody** → klik ⋮ di pojok kanannya → **Remove Component**
9. Drag `BalloonRed` dari **Hierarchy** ke folder **Prefabs** → jadi prefab!
10. Hapus `BalloonRed` dari Hierarchy (klik kanan → Delete)

### Tabel Prefab yang Harus Dibuat

| Nama Prefab | Material | Balloon Type |
|-------------|----------|--------------|
| `BalloonRed` | MatMerah | Red |
| `BalloonBlue` | MatBiru | Blue |
| `BalloonGreen` | MatHijau | Green |
| `BalloonGold` | MatEmas | Gold |

---

## LANGKAH 8 — Setup UI di GameScene

Buka **GameScene**. Buat semua elemen UI di bawah Canvas.

### 8.1 Buat Canvas

1. **GameObject > UI > Canvas**
2. Di component **Canvas Scaler**:
   - **UI Scale Mode** = `Scale With Screen Size`
   - **Reference Resolution** = `1920 x 1080`

### 8.2 ScoreText (Pojok Kiri Atas)

1. Klik kanan **Canvas** → **UI > Text - TextMeshPro**
2. Rename: `ScoreText`
3. Isi teks awal: `Skor: 0` — ukuran font 40-50
4. Posisi di pojok kiri atas (Anchor Presets: pojok kiri atas)

### 8.3 TimerText (Pojok Kanan Atas)

1. Klik kanan **Canvas** → **UI > Text - TextMeshPro**
2. Rename: `TimerText`
3. Isi teks: `Waktu: 60s`
4. Posisi di pojok kanan atas

### 8.4 Panel GameOver

1. Klik kanan **Canvas** → **UI > Panel**
2. Rename: `GameOverPanel`
3. Di dalam `GameOverPanel`, tambahkan:

| Nama | Jenis | Teks |
|------|-------|------|
| `GameOverTitle` | Text - TextMeshPro | `GAME OVER` (besar, tengah) |
| `FinalScoreText` | Text - TextMeshPro | `Skor Akhir: 0` |
| `RetryButton` | Button - TextMeshPro | `ULANGI` |
| `MenuButton` | Button - TextMeshPro | `MENU UTAMA` |

4. **Matikan GameOverPanel**: hapus centang di pojok kiri atas Inspector

### 8.5 Panel LevelUp

1. Klik kanan **Canvas** → **UI > Panel**
2. Rename: `LevelUpPanel`
3. Di dalam panel, tambahkan **Text - TextMeshPro** → rename `LevelUpText` → teks: `LEVEL 2!`
4. **Matikan LevelUpPanel**: hapus centang di Inspector

---

## LANGKAH 9 — Pasang Semua Komponen di GameScene

### 9.1 GameObject: [GameManager]

1. **GameObject > Create Empty** → rename `[GameManager]`
2. **Add Component** → cari dan pilih `GameManager`
3. Inspector: `Game Duration` = 60, `Level Up Target` = 100

### 9.2 GameObject: [UIManager]

1. **GameObject > Create Empty** → rename `[UIManager]`
2. **Add Component** → `UIManager`
3. Drag referensi ke slot (drag dari Hierarchy ke Inspector):

| Slot Inspector | Drag dari Hierarchy |
|----------------|---------------------|
| `Score Text` | `ScoreText` |
| `Timer Text` | `TimerText` |
| `Game Over Panel` | `GameOverPanel` |
| `Final Score Text` | `FinalScoreText` |
| `Level Up Panel` | `LevelUpPanel` |
| `Level Up Text` | `LevelUpText` |

### 9.3 GameObject: [BalloonSpawner]

1. **GameObject > Create Empty** → rename `[BalloonSpawner]`
2. **Add Component** → `BalloonSpawner`
3. Drag prefab dari folder **Prefabs** ke slot:

| Slot Inspector | Prefab |
|----------------|--------|
| `Balloon Red Prefab` | `BalloonRed` |
| `Balloon Blue Prefab` | `BalloonBlue` |
| `Balloon Green Prefab` | `BalloonGreen` |
| `Balloon Gold Prefab` | `BalloonGold` |

4. Nilai: `Spawn Interval` = 2, `Spawn X Range` = 4, `Spawn Y` = -6

### 9.4 GameObject: [PlayerShooter]

1. **GameObject > Create Empty** → rename `[PlayerShooter]`
2. **Add Component** → `PlayerShooter`
3. `Main Camera` → drag `Main Camera` dari Hierarchy
4. `Balloon Layer` → klik dropdown, **centang layer Balloon**

### 9.5 Hubungkan Tombol Game Over

**RetryButton:**

1. Klik `RetryButton` di Hierarchy
2. Di component **Button**, scroll ke bagian **On Click ()**
3. Klik tanda **+**
4. Drag `[GameManager]` ke slot yang muncul
5. Klik dropdown fungsi → pilih **GameManager > RetryGame()**

**MenuButton:**

1. Klik `MenuButton` → ulangi langkah di atas
2. Pilih fungsi **GameManager > GoToMainMenu()**

---

## LANGKAH 10 — Setup Main Menu Scene

Buka scene **MainMenu**.

1. Buat **Canvas** (sama seperti langkah 8.1)
2. Tambahkan:
   - **Text - TextMeshPro** → `TitleText` → teks besar: `GAME TEMBAK BALON`
   - **Button - TextMeshPro** → `PlayButton` → teks: `MULAI`
   - **Button - TextMeshPro** → `QuitButton` → teks: `KELUAR`
3. **GameObject > Create Empty** → rename `[MainMenuController]`
4. **Add Component** → `MainMenuController`
5. Hubungkan tombol:
   - `PlayButton > On Click ()` → drag `[MainMenuController]` → pilih `OnPlayButtonClicked()`
   - `QuitButton > On Click ()` → drag `[MainMenuController]` → pilih `OnQuitButtonClicked()`

---

## LANGKAH 11 — Setup Kamera GameScene

Klik **Main Camera** di Hierarchy GameScene, atur di Inspector:

- **Position**: X=0, Y=0, Z=**-10**
- **Rotation**: 0, 0, 0
- **Field of View**: 60
- **Background**: warna biru gelap (klik kotak warna di Camera)

> Kamera di Z=-10 menghadap ke depan, balon bergerak di Z=0. Ini
> adalah setup standar untuk game 3D yang terlihat seperti 2D.

---

## LANGKAH 12 — TEST GAME! 🎮

1. Buka scene **MainMenu** (double klik di panel Project)
2. Tekan tombol **▶ Play** di atas Unity
3. Klik **MULAI** → pindah ke GameScene
4. Balon mulai muncul dari bawah dan bergerak naik
5. **Klik balon** → skor bertambah, balon hilang
6. Biarkan balon lewat → tidak ada poin
7. Tunggu 60 detik → panel Game Over muncul dengan skor akhir
8. Klik **ULANGI** → game restart ✅
9. Klik **MENU UTAMA** → kembali ke MainMenu ✅

---

## Hierarki Scene yang Benar

```
GameScene
├── Main Camera           ← Position Z = -10
├── Directional Light
├── [GameManager]         ← script GameManager.cs
├── [UIManager]           ← script UIManager.cs
├── [BalloonSpawner]      ← script BalloonSpawner.cs
├── [PlayerShooter]       ← script PlayerShooter.cs
└── Canvas
    ├── ScoreText
    ├── TimerText
    ├── GameOverPanel      ← awalnya MATI (tidak aktif)
    │   ├── GameOverTitle
    │   ├── FinalScoreText
    │   ├── RetryButton
    │   └── MenuButton
    └── LevelUpPanel       ← awalnya MATI (tidak aktif)
        └── LevelUpText
```

---

## Troubleshooting Umum

| Masalah | Kemungkinan Penyebab | Solusi |
|---------|---------------------|--------|
| `NullReferenceException` terkait `Instance` | GameManager / UIManager tidak ada di scene | Cek Hierarchy: `[GameManager]` dan `[UIManager]` harus ada |
| Balon tidak muncul | Prefab belum di-drag ke BalloonSpawner | Buka Inspector BalloonSpawner, cek semua slot terisi |
| Klik tidak terdeteksi | Layer salah atau tidak ada Collider | Cek prefab: Layer = Balloon, ada Sphere Collider, tidak ada Rigidbody |
| Teks UI tidak berubah | UIManager belum dapat referensi Text | Drag ulang komponen Text ke slot UIManager di Inspector |
| Scene tidak berpindah | Nama scene salah / belum di Build Settings | Nama di kode harus PERSIS sama: `"MainMenu"` dan `"GameScene"` |
| Balon diam tidak bergerak | isPlaying = false karena GameManager error | Cek console, pastikan `[GameManager]` ada dan tidak ada error |
| Panel Game Over tidak muncul | UIManager tidak dapat referensi gameOverPanel | Pastikan GameOverPanel sudah di-drag ke Inspector UIManager |

---

## ✅ Core Loop yang Sudah Berjalan

- [x] Main Menu → klik MULAI → masuk GameScene
- [x] Balon spawn acak dari bawah layar
- [x] Balon bergerak naik + berayun (simulasi arus laut)
- [x] Klik balon = tembak (raycast)
- [x] Kena balon → skor bertambah, balon hancur
- [x] Meleset → tidak ada poin
- [x] Timer hitung mundur 60 detik
- [x] Level Up → spawn makin cepat, notifikasi muncul
- [x] Timer habis → Game Over, skor akhir tampil
- [x] Retry → game restart dari awal
- [x] Menu Utama → kembali ke MainMenu

---

## Pengembangan Selanjutnya (Opsional)

Setelah core loop jalan, kamu bisa tambahkan:

1. **Efek Pop Balon**: Buat Particle System (GameObject > Effects > Particle System),
   simpan sebagai prefab, lalu `Instantiate(popEffect, transform.position, Quaternion.identity)`
   di dalam `OnHit()` sebelum `Destroy(gameObject)`

2. **Sound Effect**: Tambahkan `AudioSource` di script, lalu
   `AudioSource.PlayClipAtPoint(clip, transform.position)` saat balon kena

3. **High Score**: Di GameManager, setelah game over tambahkan:
   ```csharp
   PlayerPrefs.SetInt("HighScore", Mathf.Max(score, PlayerPrefs.GetInt("HighScore", 0)));
   ```

4. **Background Laut**: Import gambar sebagai Sprite, taruh di scene dengan Z=5

5. **Mobile Touch**: Ganti `Input.GetMouseButtonDown(0)` dengan sentuhan:
   ```csharp
   if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
       Shoot();
   ```

6. **Animasi Balon Keluar**: Sebelum `Destroy()`, jalankan animasi scale atau
   tween menggunakan DOTween (asset gratis di Unity Asset Store)
