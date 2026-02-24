export const TokenService = {
  getAccessToken: () => localStorage.getItem("authToken"),
  getRefreshToken: () => localStorage.getItem("refreshToken"),
  setAccessToken: (token: string) => localStorage.setItem("authToken", token),
  clearTokens: () => {
    localStorage.removeItem("authToken");
    localStorage.removeItem("refreshToken");
  },
};
