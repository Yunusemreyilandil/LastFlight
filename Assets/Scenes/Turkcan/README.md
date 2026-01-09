# The Happy Prince - Main Menu UI

Bu klasör Türkcan'ın çalışma alanıdır ve "The Happy Prince" oyununun ana menü arayüzünü içerir.

## 📁 Klasör Yapısı

```
Turkcan/
├── Scripts/           # C# scriptleri
│   ├── MainMenuManager.cs      # Ana menü yöneticisi
│   ├── ButtonAnimator.cs       # Buton animasyonları
│   ├── SettingsManager.cs      # Ayarlar paneli yöneticisi
│   └── FadeTransition.cs       # Sahne geçiş efektleri
├── Resources/         # Görseller ve ses dosyaları
│   └── UI/           # UI görselleri buraya
├── Prefabs/          # UI prefab'ları
└── MainMenu.unity    # Ana menü sahnesi
```

## 🎮 Kurulum Adımları

### 1. Görselleri İçe Aktarma
Figma'dan export ettiğiniz görselleri `Assets/Scenes/Turkcan/Resources/UI/` klasörüne ekleyin:
- Background.png (Arka plan)
- Title.png (THE HAPPY PRINCE başlığı)
- PlayButton.png (PLAY butonu)
- SettingsButton.png (SETTINGS butonu)
- ExitButton.png (EXIT butonu)
- Bird.png (Kuş dekorasyonu)
- Book.png (Kitap dekorasyonu)
- Medallion.png (Madalyon dekorasyonu)

### 2. Unity'de Scene Açma
1. Unity'de `Assets/Scenes/Turkcan/MainMenu.unity` dosyasını açın
2. Hierarchy'de Canvas objesini seçin
3. Inspector'da görselleri ilgili Image componentlerine sürükleyin

### 3. Buton Bağlantıları
1. **Canvas > MainMenuPanel > PlayButton** seçin
   - Inspector'da `Button Animator` component'i otomatik eklenmiş olmalı
   - `MainMenuManager` scriptinde `Play Button` alanına bu butonu sürükleyin

2. **SettingsButton** ve **ExitButton** için aynı işlemi tekrarlayın

### 4. Ses Dosyaları (Opsiyonel)
Ses efektleri eklemek için:
1. Ses dosyalarını `Resources/UI/` klasörüne ekleyin
2. `MainMenuManager` component'inde:
   - `Button Click Sound` alanına tıklama sesi
   - `Button Hover Sound` alanına hover sesi ekleyin

## 🎨 UI Özellikleri

### MainMenuManager
- **Play Button**: Oyun sahnesine geçiş yapar
- **Settings Button**: Ayarlar panelini açar
- **Exit Button**: Oyundan çıkış yapar

### ButtonAnimator
Her butona otomatik olarak eklenir ve şu animasyonları sağlar:
- **Hover**: Buton üzerine gelindiğinde büyüme (1.1x)
- **Press**: Butona basıldığında küçülme (0.95x)
- **Color**: Hover'da renk değişimi (opsiyonel)

### SettingsManager
Ayarlar panelinde:
- Master/Music/SFX ses kontrolleri
- Grafik kalitesi ayarları
- Tam ekran/Pencere modu
- Çözünürlük seçimi

### FadeTransition
Sahne geçişlerinde yumuşak fade in/out efektleri

## 🔧 Özelleştirme

### Buton Animasyonlarını Değiştirme
`ButtonAnimator` component'inde:
- `Hover Scale`: Hover'da büyüme oranı (varsayılan: 1.1)
- `Press Scale`: Basıldığında küçülme oranı (varsayılan: 0.95)
- `Animation Duration`: Animasyon süresi (varsayılan: 0.2s)
- `Animation Ease`: Animasyon eğrisi (varsayılan: OutBack)

### Oyun Sahnesini Değiştirme
`MainMenuManager` component'inde:
- `Game Scene Name`: Yüklenecek sahne adını girin

## 📝 Notlar

- **DOTween Gereksinimi**: `ButtonAnimator` ve `FadeTransition` scriptleri DOTween kullanır. 
  - Package Manager > Add package from git URL: `https://github.com/Demigiant/dotween.git`
  - Veya Asset Store'dan DOTween (Free) indirin

- **TextMeshPro**: Settings paneli için TextMeshPro gereklidir (Unity'de varsayılan olarak gelir)

## 🐛 Sorun Giderme

**DOTween hatası alıyorsanız:**
1. Window > Package Manager
2. + > Add package from git URL
3. `https://github.com/Demigiant/dotween.git` yazın

**Butonlar çalışmıyorsa:**
1. Canvas > Event System objesinin var olduğundan emin olun
2. Button component'lerinin `Interactable` olduğunu kontrol edin
3. MainMenuManager'da buton referanslarının atandığını kontrol edin

## 👤 Geliştirici
Türkcan - The Happy Prince Main Menu UI
