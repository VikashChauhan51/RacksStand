import { MyRacksStandPage } from './app.po';

describe('my-racks-stand App', () => {
  let page: MyRacksStandPage;

  beforeEach(() => {
    page = new MyRacksStandPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
