import { test, expect } from '@playwright/test';

test('basic test', async ({ page }) => {
  await page.goto('src/');
  await page.waitForSelector('header');
  await expect(page.locator('header')).toContainText('home');
})
