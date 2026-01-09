# 🎮 The Happy Prince - Unity Kurulum Kılavuzu

## ⚡ Hızlı Başlangıç

### 1. Görselleri Hazırlama
Figma'dan görselleri export edin (PNG formatında, şeffaf arka plan):

**Gerekli Görseller:**
- `Background.png` - Arka plan (1920x1080 önerilir)
- `Title.png` - "THE HAPPY PRINCE" başlığı
- `PlayButton.png` - PLAY butonu
- `SettingsButton.png` - SETTINGS butonu  
- `ExitButton.png` - EXIT butonu
- `Bird.png` - Kuş dekorasyonu (sağ üst)
- `Book.png` - Açık kitap (alt orta)
- `Medallion.png` - Madalyon (sağ alt)

### 2. Unity'ye İçe Aktarma
1. Tüm görselleri `Assets/Scenes/Turkcan/Resources/UI/` klasörüne kopyalayın
2. Unity'de görselleri seçin ve Inspector'da:
   - **Texture Type**: Sprite (2D and UI)
   - **Pixels Per Unit**: 100
   - Apply butonuna tıklayın

### 3. DOTween Kurulumu (ÖNEMLİ!)
Buton animasyonları için DOTween gereklidir:

**Yöntem 1 - Package Manager (Önerilen):**
```
1. Unity'de Window > Package Manager
2. Sol üstteki + butonuna tıklayın
3. "Add package from git URL" seçin
4. Şunu yapıştırın: com.demigiant.dotween
5. Add butonuna tıklayın
```

**Yöntem 2 - Asset Store:**
```
1. Window > Asset Store
2. "DOTween" arayın
3. DOTween (Free) indirin ve import edin
```

### 4. Scene'i Açma ve Düzenleme

#### A. Scene'i Aç
1. Unity Project penceresinde `Assets/Scenes/Turkcan/MainMenu.unity` dosyasına çift tıklayın
2. Scene açıldığında Hierarchy'de şunları görmelisiniz:
   - Main Camera
   - Canvas
   - EventSystem

#### B. UI Elementlerini Oluşturma

**Canvas Ayarları:**
1. Canvas'ı seçin
2. Inspector'da Canvas Scaler component'inde:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080
   - Match: 0.5 (Width/Height arası)

**MainMenuPanel İçine Elementler Ekleyin:**

1. **Arka Plan Ekle:**
   ```
   Hierarchy'de sağ tık > UI > Image
   İsim: Background
   - Anchor: Stretch (Alt+Shift tuşlarına basılı tutarak sağ alttaki kareye tıklayın)
   - Left, Right, Top, Bottom: 0
   - Source Image: Background.png sürükleyin
   ```

2. **Başlık Ekle:**
   ```
   Hierarchy'de MainMenuPanel'e sağ tık > UI > Image
   İsim: Title
   - Anchor: Top Center
   - Pos Y: -150
   - Width: 600, Height: 150
   - Source Image: Title.png
   ```

3. **Play Butonu Ekle:**
   ```
   Hierarchy'de MainMenuPanel'e sağ tık > UI > Button - TextMeshPro
   İsim: PlayButton
   - Anchor: Middle Center
   - Pos Y: 50
   - Width: 200, Height: 80
   - Source Image: PlayButton.png
   - Text child'ını silin (görsel zaten "PLAY" yazıyor)
   
   Inspector'da Add Component:
   - ButtonAnimator scripti ekleyin
   ```

4. **Settings Butonu Ekle:**
   ```
   PlayButton'u kopyalayın (Ctrl+D)
   İsim: SettingsButton
   - Pos Y: -50
   - Source Image: SettingsButton.png
   ```

5. **Exit Butonu Ekle:**
   ```
   SettingsButton'u kopyalayın (Ctrl+D)
   İsim: ExitButton
   - Pos Y: -150
   - Source Image: ExitButton.png
   ```

6. **Dekoratif Elementler:**
   ```
   Kuş (Sağ Üst):
   - UI > Image, İsim: Bird
   - Anchor: Top Right
   - Pos X: -100, Pos Y: -100
   - Width: 150, Height: 100
   - Source Image: Bird.png
   
   Kitap (Alt Orta):
   - UI > Image, İsim: Book
   - Anchor: Bottom Center
   - Pos Y: 100
   - Width: 400, Height: 250
   - Source Image: Book.png
   
   Madalyon (Sağ Alt):
   - UI > Image, İsim: Medallion
   - Anchor: Bottom Right
   - Pos X: -50, Pos Y: 50
   - Width: 100, Height: 100
   - Source Image: Medallion.png
   ```

#### C. MainMenuManager Bağlantıları

1. **Canvas > MainMenuPanel** seçin
2. Inspector'da MainMenuManager component'inde:
   - **Play Button**: PlayButton'u sürükleyin
   - **Settings Button**: SettingsButton'u sürükleyin
   - **Exit Button**: ExitButton'u sürükleyin
   - **Main Menu Panel**: MainMenuPanel'i sürükleyin
   - **Game Scene Name**: Oyun sahnenizin adını yazın (örn: "GameScene")

#### D. Audio Ekleme (Opsiyonel)

1. Hierarchy'de Canvas'a sağ tık > Audio > Audio Source
2. Inspector'da:
   - Play On Awake: KAPALI
   - Loop: KAPALI
3. MainMenuManager component'inde:
   - **Audio Source**: Oluşturduğunuz Audio Source'u sürükleyin
   - **Button Click Sound**: Tıklama ses dosyanızı sürükleyin
   - **Button Hover Sound**: Hover ses dosyanızı sürükleyin

### 5. Test Etme

1. Play butonuna basın (Ctrl+P)
2. Butonların üzerine geldiğinizde animasyon olmalı
3. Butonlara tıkladığınızda Console'da log mesajları görmelisiniz

## 🎨 Özelleştirme

### Buton Animasyonları
Her butonda `ButtonAnimator` component'i var. Ayarlar:
- **Hover Scale**: 1.1 (Hover'da %10 büyüme)
- **Press Scale**: 0.95 (Basıldığında %5 küçülme)
- **Animation Duration**: 0.2 saniye
- **Animation Ease**: OutBack (Elastik efekt)
- **Change Color On Hover**: true
- **Hover Color**: Açık sarı/krem ton

### Renk Değiştirme
ButtonAnimator'da `Change Color On Hover` kapalıysa sadece scale animasyonu olur.

## 🐛 Sorun Giderme

### "DOTween could not be found" Hatası
- DOTween'i Package Manager'dan yükleyin (yukarıdaki adım 3)
- Unity'yi yeniden başlatın

### Butonlar Çalışmıyor
- EventSystem objesinin var olduğunu kontrol edin
- Button component'lerinin Interactable olduğunu kontrol edin
- MainMenuManager'da buton referanslarının atandığını kontrol edin

### Görseller Bulanık
- Görselleri seçin, Inspector'da:
  - Filter Mode: Point (no filter) veya Bilinear
  - Compression: None
  - Max Size: 2048 veya 4096

### Scene Yüklenmiyor
- Build Settings'e (Ctrl+Shift+B) gidin
- "Add Open Scenes" butonuna tıklayın
- MainMenu sahnesini listeye ekleyin

## 📋 Checklist

- [ ] DOTween kuruldu
- [ ] Görseller Resources/UI/ klasörüne eklendi
- [ ] Görseller Sprite olarak ayarlandı
- [ ] MainMenu.unity açıldı
- [ ] Canvas ayarları yapıldı (1920x1080)
- [ ] Background eklendi
- [ ] Title eklendi
- [ ] 3 buton eklendi (Play, Settings, Exit)
- [ ] Her butona ButtonAnimator eklendi
- [ ] Dekoratif elementler eklendi (Bird, Book, Medallion)
- [ ] MainMenuManager'da butonlar bağlandı
- [ ] Game Scene Name ayarlandı
- [ ] Test edildi ve çalışıyor

## 🎯 Sonraki Adımlar

1. **Settings Panel Oluşturma**: SettingsManager scripti hazır, UI'ı oluşturmanız gerekiyor
2. **Ses Efektleri**: Buton sesleri ekleyin
3. **Arka Plan Müziği**: AudioSource ile loop müzik ekleyin
4. **Fade Transition**: Sahne geçişlerinde FadeTransition kullanın
5. **Oyun Sahnesi**: Asıl oyun sahnenizi oluşturun

## 💡 İpuçları

- Görselleri Figma'dan 2x veya 3x boyutunda export edin (daha keskin görünür)
- Butonları test ederken Game view'da test edin (Scene view'da çalışmazlar)
- Settings paneli için ayrı bir Panel objesi oluşturun ve başta kapalı tutun
- Tüm UI elementlerini MainMenuPanel içinde tutun (organizasyon için)

---
**Hazırlayan:** Türkcan  
**Proje:** The Happy Prince  
**Tarih:** 9 Ocak 2026
