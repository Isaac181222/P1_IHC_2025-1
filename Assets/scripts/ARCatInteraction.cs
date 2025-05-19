using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ARCatInteraction : MonoBehaviour
{
    public Animator animator;
    public TMP_Text phraseText;
    public float phraseDisplayTime = 3f;

    private AudioSource audioSource; // 🎵 Audio del maullido

    private List<string> phrases = new List<string>
    {
        "Respira profundamente",
        "Suelta tus pensamientos",
        "Vuelve al presente",
        "Escucha tu interior",
        "Relaja los hombros",
        "Acepta este momento",
        "Deja ir lo que no puedes controlar",
        "Siente el aire entrar y salir",
        "Todo está bien aquí y ahora",
        "Abraza el silencio",
        "Permítete sentir paz",
        "Fluye con cada respiración",
        "Eres suficiente tal como eres",
        "Encuentra calma en cada exhalación",
        "Observa sin juzgar",
        "Conecta con tu esencia",
        "El presente es tu hogar",
        "Habita tu cuerpo con amabilidad",
        "Escucha el latido de tu corazón",
        "Confía en el proceso de la vida",
        "Estás exactamente donde necesitas estar",
        "Permanece en la quietud",
        "Tu mente puede descansar ahora",
        "Siente la paz dentro de ti",
        "El ahora es todo lo que necesitas",
        "Permite que todo sea como es",
        "Eres parte del todo",
        "Calma tu mente, abre tu corazón",
        "Cada respiración es una nueva oportunidad",
        "Estás en paz, estás en calma",
        "Respira, suelta, confía",
        "Siéntete seguro en este momento",
        "Todo está en armonía",
        "Déjate llevar por la quietud",
        "Tu ser interior brilla en silencio"
    };

    private string[] triggers =
    {
        "walk", "run", "jump", "Greeting", "Eat", "Sleep01", "Sleep02", "Sleep03"
    };

    private bool isAnimating = false;

    void Start()
    {
        if (phraseText != null)
            phraseText.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform && !isAnimating)
                {
                    TriggerInteraction();
                }
            }
        }
    }

    void TriggerInteraction()
    {
        isAnimating = true;

        string trigger = triggers[Random.Range(0, triggers.Length)];
        string phrase = phrases[Random.Range(0, phrases.Count)];

        animator.SetTrigger(trigger);

        // 🎵 Reproducir sonido de maullido si hay audio asignado
        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();

        phraseText.text = phrase;
        phraseText.gameObject.SetActive(true);

        Invoke(nameof(ResetToIdle), phraseDisplayTime);
    }

    void ResetToIdle()
    {
        phraseText.gameObject.SetActive(false);
        isAnimating = false;
    }
}
