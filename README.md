```
public class Solution {
    public int MinAreaRect(int[][] points) {
        int len = 500;
        int ans = 0;
        for(int i = 0; i < len; i++) {
            for(int j = 0; j < len; j++) {
                if(points[i][j] != 0) {
                    for(int k = 0; k < (len - i); k++) {
                        if(points[i+k][j] != 0) {
                            for(int l = 0; l < (len - j); l++) {
                                if(points[i+k][j+l] != 0 && points[i][j+l] != 0) {
                                    int temp = k * l;
                                    if(temp < ans) ans = temp;
                                }
                            }
                        }
                    }
                };
            }
        }
        return ans;
    }
}
```
