using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GraphicsSettings : MonoBehaviour
{
    public static GraphicsSettings Instance;

    // Referências dos elementos da UI (arrastar no Inspector)
    public Dropdown resolutionDropdown;
    public Dropdown screenModeDropdown;
    public Toggle vSyncToggle;
    public Dropdown qualityDropdown;
    public Dropdown fpsDropdown;
    public Slider brightnessSlider;

    public GameObject ConfigMenu;

    // Array que guarda todas as resoluções disponíveis no sistema
    Resolution[] resolutions;
void Start()
    {
        // Configura cada parte do menu ao iniciar
        SetupResolutions();
        SetupQuality();
        SetupFPS();

        // Carrega configurações salvas anteriormente
        LoadSettings();
        SetupScreenMode();
    }

   
    void SetupResolutions()
    {
        // Pega todas as resoluções suportadas pelo monitor
        resolutions = Screen.resolutions;

        // Limpa opções antigas do Dropdown
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;

        // Percorre todas as resoluções disponíveis
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Cria texto no formato "1920 x 1080"
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Verifica qual é a resolução atual
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        // Adiciona as opções no Dropdown
        resolutionDropdown.AddOptions(options);

        // Define como selecionada a resolução atual
        resolutionDropdown.value = currentResIndex;
    }

    void SetupQuality()
    {
        // Limpa opções antigas
        qualityDropdown.ClearOptions();

        // Adiciona os nomes das qualidades definidas no Project Settings
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));

        // Define como selecionada a qualidade atual
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    void SetupScreenMode()
    {
        screenModeDropdown.ClearOptions();

        screenModeDropdown.AddOptions(new List<string>
    {
        "Windowed",
        "Borderless",
        "Fullscreen"
    });

        // Detecta modo atual
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.Windowed:
                screenModeDropdown.value = 0;
                break;

            case FullScreenMode.FullScreenWindow:
                screenModeDropdown.value = 1;
                break;

            case FullScreenMode.ExclusiveFullScreen:
                screenModeDropdown.value = 2;
                break;
        }
    }


    void SetupFPS()
    {
        // Limpa opções antigas
        fpsDropdown.ClearOptions();

        // Adiciona limites de FPS personalizados
        fpsDropdown.AddOptions(new List<string>
        {
            "30",
            "60",
            "120",
            "Unlimited"
        });
    }

    // Função chamada pelo botão "Apply"
    public void ApplySettings()
    {
        // ---------- RESOLUÇÃO ----------
        Resolution res = resolutions[resolutionDropdown.value];

        // Define nova resolução e modo fullscreen
        FullScreenMode mode = FullScreenMode.Windowed;

        switch (screenModeDropdown.value)
        {
            case 0:
                mode = FullScreenMode.Windowed;
                break;

            case 1:
                mode = FullScreenMode.FullScreenWindow; // Borderless
                break;

            case 2:
                mode = FullScreenMode.ExclusiveFullScreen;
                break;
        }

        Screen.SetResolution(res.width, res.height, mode);

        // ---------- QUALIDADE ----------
        QualitySettings.SetQualityLevel(qualityDropdown.value);

        // ---------- VSYNC ----------
        // 1 = ligado | 0 = desligado
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;

        // ---------- FPS LIMIT ----------
        switch (fpsDropdown.value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;

            case 1:
                Application.targetFrameRate = 60;
                break;

            case 2:
                Application.targetFrameRate = 120;
                break;

            case 3:
                Application.targetFrameRate = -1; // Sem limite
                break;
        }

        // Salva as configurações
        SaveSettings();
    }

    void SaveSettings()
    {
        // Salva cada configuração usando PlayerPrefs

        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        if (PlayerPrefs.HasKey("ScreenMode"))
            screenModeDropdown.value = PlayerPrefs.GetInt("ScreenMode");
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
        PlayerPrefs.SetInt("VSync", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("FPS", fpsDropdown.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);

        // Força salvar em disco
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        // Se existir configuração salva, carrega

        if (PlayerPrefs.HasKey("Resolution"))
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");

        if (PlayerPrefs.HasKey("ScreenMode"))
            screenModeDropdown.value = PlayerPrefs.GetInt("ScreenMode");

        if (PlayerPrefs.HasKey("Quality"))
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");

        if (PlayerPrefs.HasKey("VSync"))
            vSyncToggle.isOn = PlayerPrefs.GetInt("VSync") == 1;

        if (PlayerPrefs.HasKey("FPS"))
            fpsDropdown.value = PlayerPrefs.GetInt("FPS");

        if (PlayerPrefs.HasKey("Brightness"))
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
    }
    public void ResetSettings()
    {
        // ---------- RESOLUÇÃO ----------
        // Define como padrão a resolução atual do monitor
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        // ---------- MODO DE TELA ----------
        // Padrão recomendado: Borderless
        screenModeDropdown.value = 1; // 0 = Windowed | 1 = Borderless | 2 = Fullscreen

        // ---------- QUALIDADE ----------
        // Define qualidade média (ajuste como quiser)
        qualityDropdown.value = 2;

        // ---------- VSYNC ----------
        vSyncToggle.isOn = true;

        // ---------- FPS ----------
        fpsDropdown.value = 1; // 60 FPS

        // ---------- BRILHO ----------
        brightnessSlider.value = 1f; // valor padrão

        // Aplica tudo imediatamente
        ApplySettings();
    }
}
