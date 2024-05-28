export class FilterDTO {
  public categories: string[];
  public publishingHouses: string[];
  public languages: string[];

  constructor(
    categories: string[],
    publishingHouses: string[],
    languages: string[]
  ) {
    this.categories = categories;
    this.publishingHouses = publishingHouses;
    this.languages = languages;
  }
}
