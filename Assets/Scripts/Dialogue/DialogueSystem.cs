using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    bool isDialogueBoxOpen;
    bool canSpeak;
    bool isDisplayingSentence;
    bool isDisplayingDialogue;

    [Header("UI")]
    [SerializeField] GameObject UIPanel;
    [SerializeField] TextMeshProUGUI dialogueText;


    [Header("Sounds")]
    [SerializeField] AudioClip transmissionOpen;
    [SerializeField] AudioClip transmissionClose;

    Animator panelAnimator;
    AudioSource audioSource;
    CanvasGroup canvasGroup;

    List<Dialogue> dialogueToPlay = new List<Dialogue>();

    Dialogue currentDialogueActive;
    int currentPriority = 0;

    public static Action<Dialogue> OnPlaySubtitles;

    readonly int anim_openDialogue = Animator.StringToHash("OpenDialogue");
    readonly int anim_closeDialogue = Animator.StringToHash("CloseDialogue");

    void Start()
    {
        panelAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        canvasGroup = UIPanel.GetComponent<CanvasGroup>();

        OnPlaySubtitles += PlaySubtitles;
    }

    public void PlaySubtitles(Dialogue subs)
    {
        if (canvasGroup.alpha == 0)
            canvasGroup.alpha = 1;

        if (subs.priority > currentPriority)
        {
            StopAllCoroutines();
            StartCoroutine(HighPriorityDialogue(subs));
        } else
        {
            dialogueToPlay.Add(subs);
            if (!isDialogueBoxOpen)
                StartCoroutine(PlayEachDialogue());
        }
    }

    // added for testing
    public void PlaySpeech(Dialogue subs)
    {
        dialogueToPlay.Add(subs);
        StartCoroutine(PlayDialogueSound());
    }

    private IEnumerator HighPriorityDialogue(Dialogue priorityDialogue)
    {
        // Close current dialogue playing, remote it from the list and clean variables
        if (canSpeak)
            AnimationDialogueBox(anim_closeDialogue, transmissionClose);

        StopTalk(true);
        dialogueToPlay.Remove(currentDialogueActive);
        isDisplayingDialogue = false;
        isDisplayingSentence = false;

        // Clean text box
        dialogueText.text = "";

        StopTalk(false);

        yield return new WaitUntil(() => !isDialogueBoxOpen);

        // Cache current dialogue and its priority
        currentDialogueActive = priorityDialogue;
        currentPriority = priorityDialogue.priority;

        // Set character speaking and open the dialogue box
        panelAnimator.SetInteger("CharID", priorityDialogue.charId);
        AnimationDialogueBox(anim_openDialogue, transmissionOpen);

        // Wait until the dialogue box is completely open
        yield return new WaitUntil(() => canSpeak);

        StartCoroutine(PlayEachSentence(priorityDialogue));
        isDisplayingDialogue = true;

        // Wait until finish all the sentences from the dialogue
        yield return new WaitUntil(() => !isDisplayingDialogue);

        // Remove dialogue finished from the list and close the dialogue box
        dialogueToPlay.Remove(priorityDialogue);
        AnimationDialogueBox(anim_closeDialogue, transmissionClose);
        StopTalk(false);

        // Wait until the dialogue box is completely close
        yield return new WaitUntil(() => !isDialogueBoxOpen);

        // Play the pending dialogues
        if (dialogueToPlay.Count > 0)
            StartCoroutine(PlayEachDialogue());
    }

    private IEnumerator PlayEachDialogue()
    {
        while (dialogueToPlay.Count > 0)
        {
            int length = dialogueToPlay.Count;

            // Loop for each dialogue
            for (int i = 0; i < length; i++)
            {
                // Clean text box
                dialogueText.text = "";

                // Cache current dialogue and its priority
                currentDialogueActive = dialogueToPlay[i];
                currentPriority = dialogueToPlay[i].priority;

                // Set character speaking and open the dialogue box
                panelAnimator.SetInteger("CharID", dialogueToPlay[i].charId);
                AnimationDialogueBox(anim_openDialogue, transmissionOpen);

                yield return new WaitUntil(() => canSpeak);

                StartCoroutine(PlayEachSentence(dialogueToPlay[i]));
                isDisplayingDialogue = true;

                //Wait until finish all the sentences from the dialogue
                yield return new WaitUntil(() => !isDisplayingDialogue);

                // Remove dialogue finished from the list and close the dialogue box
                dialogueToPlay.Remove(dialogueToPlay[i]);
                AnimationDialogueBox(anim_closeDialogue, transmissionClose);
                StopTalk(false);

                // Wait until the dialogue box is completely close
                yield return new WaitUntil(() => !isDialogueBoxOpen);
            }
        }
    }

    private IEnumerator PlayDialogueSound()
    {
        while (dialogueToPlay.Count > 0)
        {
            int length = dialogueToPlay.Count;

            // Loop for each dialogue
            for (int i = 0; i < length; i++)
            {
                // Clean text box
                dialogueText.text = "";

                // Cache current dialogue and its priority
                currentDialogueActive = dialogueToPlay[i];
                currentPriority = dialogueToPlay[i].priority;

                // Set character speaking and open the dialogue box
                /*panelAnimator.SetInteger("CharID", dialogueToPlay[i].charId);
                AnimationDialogueBox(anim_openDialogue, transmissionOpen);*/

                // yield return new WaitUntil(() => canSpeak);

                StartCoroutine(PlayEachSentence(dialogueToPlay[i]));
                isDisplayingDialogue = true;

                //Wait until finish all the sentences from the dialogue
                // yield return new WaitUntil(() => !isDisplayingDialogue);

                // Remove dialogue finished from the list and close the dialogue box
                dialogueToPlay.Remove(dialogueToPlay[i]);
                // AnimationDialogueBox(anim_closeDialogue, transmissionClose);
                StopTalk(false);

                // Wait until the dialogue box is completely close
                // yield return new WaitUntil(() => !isDialogueBoxOpen);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private IEnumerator PlayEachSentence(Dialogue dialogue)
    {
        // Loop for each sentence in the dialogue
        for (int i = 0; i < dialogue.subtitles.Length; i++)
        {
            StartCoroutine(LetterByLetter(dialogue.subtitles[i].text, .03f));

            audioSource.clip = dialogue.subtitles[i].speech;
            audioSource.Play();

            isDisplayingSentence = true;

            yield return new WaitUntil(() => !isDisplayingSentence);
        }

        yield return new WaitForSeconds(1f);

        isDisplayingDialogue = false;
    }

    private IEnumerator LetterByLetter(string text, float delay)
    {
        // Play talk animation if it's stopped
        StopTalk(false);

        // Loop showing letter by letter of sentence
        for (int i = 0; i < text.Length; i++)
        {
            string currentText = text.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(0.7f);

        // Stop talk animation
        StopTalk(true);

        yield return new WaitForSeconds(.3f);

        isDisplayingSentence = false;
    }

    private void StopTalk(bool state)
    {
        if (state)
            panelAnimator.SetTrigger("Reset");

        panelAnimator.speed = state ? 0 : 1;
    }

    private void AnimationDialogueBox(int animHash, AudioClip soundClip)
    {
        panelAnimator.SetTrigger(animHash);
        audioSource.clip = soundClip;
        audioSource.Play();
    }

    public void EnableCanSpeak() { canSpeak = true; }
    public void DisableCanSpeak() { canSpeak = false; }

    public void EnableIsDialogueBoxOpen() { isDialogueBoxOpen = true; }
    public void DisableDialogueBoxOpen() { isDialogueBoxOpen = false; }
}
