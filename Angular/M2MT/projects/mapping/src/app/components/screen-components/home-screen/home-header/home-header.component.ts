import { Component, OnInit } from '@angular/core';

enum Themes {
  DEFAULT="default",
  LIGHT="light-mode",
  DARK="dark-mode",
}

@Component({
  selector: 'app-home-header',
  templateUrl: './home-header.component.html',
  styleUrls: ['./home-header.component.scss']
})
export class HomeHeaderComponent implements OnInit {
  public static readonly THEME_STORAGE_NAME = "THEME_STORAGE";
  readonly themeOptions: Themes[] = [
    Themes.DARK,
    Themes.DEFAULT,
    Themes.LIGHT,
  ]

  theme: Themes = Themes.DEFAULT;

  menuOpen = false;

  ngOnInit(): void {
    const inStorage = localStorage.getItem(HomeHeaderComponent.THEME_STORAGE_NAME);
    if (!inStorage) {
      this.theme = Themes.DEFAULT;
      return;
    }

    this.theme = <Themes> inStorage;
    this.updateThem(<Themes> inStorage)
  }

  updateThem(newTheme: Themes) {
    if (this.theme !== Themes.DEFAULT) document.body.classList.remove(this.theme);
    this.theme = newTheme;
    document.body.classList.add(this.theme);
    localStorage.setItem(HomeHeaderComponent.THEME_STORAGE_NAME, this.theme);
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }
}
