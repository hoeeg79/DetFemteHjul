import {Component} from '@angular/core';
import {StateService} from '../state.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  toTranslate?: string;
  chosenLanguage?: string = "Afrikaans";
  fromLanguage?: string;
  fromLanguageCode?: string;
  languageCode?: string;

  constructor(public state: StateService) {
  }

  onTranlate() {
    this.findLanguageCode()
    var dto = {
      eventType: "ClientWantsToTranslate",
      messageToTranslate: this.toTranslate,
      toLanguage: this.languageCode,
      fromLanguage: this.fromLanguageCode,
    }
    this.state.ws.send(JSON.stringify(dto));
  }

  findLanguageCode() {
    for (let i = 0; i < this.state.languages!.length; i++) {
      if (this.state.languages![i] === this.chosenLanguage) {
        this.languageCode = this.state.code![i];
        break
      }
    }
    for (let i = 0; i < this.state.fromLanguages!.length; i++) {
      if (this.state.fromLanguages![i] === this.fromLanguage) {
        this.fromLanguageCode = this.state.code![i];
        break
      }
    }
  }

  async onMicrophone() {
    try {
      this.findLanguageCode();
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
      const mediaRecorder = new MediaRecorder(stream);
      const audioChunks: BlobPart[] = [];

      mediaRecorder.addEventListener("dataavailable", (event) => {
        audioChunks.push(event.data);
      });

      mediaRecorder.addEventListener("stop", () => {
        const audioBlob = new Blob(audioChunks, { type: "audio/wav" });
        // Now you have the audioBlob containing the recorded audio.
        console.log("audioBlob: ", audioBlob);
        // Send the audioBlob to your WebSocket server.
        let byteArray: Uint8Array;
        const reader = new FileReader();
        reader.onload = function(event) {
          const arrayBuffer = event.target!.result as ArrayBuffer;
          byteArray = new Uint8Array(arrayBuffer);
          console.log(byteArray)
          return byteArray;
        };
        reader.readAsArrayBuffer(audioBlob);
        console.log("byteArray: ", byteArray!);

      });

      // Start recording.
      mediaRecorder.start();

      // Stop recording after 5 seconds (adjust as needed).
      setTimeout(() => {
        mediaRecorder.stop();
      }, 6500);
    } catch (error) {
      console.error("Error accessing microphone:", error);
    }
    /*navigator.mediaDevices.getUserMedia({audio: true})
      .then(stream => {
        const mediaRecorder = new MediaRecorder(stream);
        mediaRecorder.start();
        const audioChunks: BlobPart[] | undefined = [];

        mediaRecorder.addEventListener("dataavailable", event => {
          audioChunks.push(event.data);
        });
        mediaRecorder.addEventListener("stop", () => {
          const audioBlob = new Blob(audioChunks);
          const audioUrl = URL.createObjectURL(audioBlob);
          const audio = new Audio(audioUrl);
          audio.play();
        });
        setTimeout(() => {
          mediaRecorder.stop();
        }, 5000);
      });*/
  }
}
