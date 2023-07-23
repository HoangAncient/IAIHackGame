using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// const int POINT = 10;
// const int TimeForBonus = 60;
// const float TimeBonusWeight = (float)1/6;
// const float PointIn2ndPartWeight = 0.7; 

// namespace QuizNamespace{
public class PointCalculator : MonoBehaviour
{
    // Đang ở trong Part 1
    public static bool Part1 = true;

    // Đang ở trong Part 2
    public static bool Part2 = false;

    // Tổng điểm hiện có
    public static int currentPoint = 0;

    // Điểm gốc
    const int BasePOINT = 10;

    // Thời gian cho bonus
    const int TimeForBonus = 60;

    // Hệ số điểm bonus về thời gian
    const float TimeBonusWeight = (float)1/6;

    // Hệ số điểm của part 2 so với part 1
    const float PointIn2ndPartWeight = (float)0.7; 

    // Kiểm tra xem có dùng trợ giúp Ngôi sao hi vọng không
    public static bool STAR = false;

    // Kiểm tra xem có dùng trợ giúp bảo vệ chuỗi không
    public static bool StreakProtection = false;

    public static int currentStreak;
    // public static int currentStreak = 0;

    int TimeBonusPoint(int timeToAnswer) {
        if (timeToAnswer < TimeForBonus) {
            return Mathf.FloorToInt((TimeForBonus - timeToAnswer)*(TimeBonusWeight));
        }
        return 0;
    }

    int  StreakBonus(int point) {
        int bonusPercent = 0;
        if(currentStreak == 3) {
            bonusPercent = 30;
        }
        else if (currentStreak == 4) {
            bonusPercent = 40;
        }
        else if (currentStreak >= 5) {
            bonusPercent = 50;
        }
        return point*bonusPercent;
    }

    int StarBonus(int timeToAnswer) {
        if (STAR) {
            return 2*(BasePOINT + StreakBonus(BasePOINT));
        }
        return 0;
    }

    int PointPerQuestion(int BasePoint) {
        // if use Star

        // if use streakProtection

        
        int Point = BasePoint;
        return Point + + StreakBonus(Point);
    }

    int TotalPoint() {
        int totalPoint = 0;
        for(int i = 0; i < QuizManager.questions.Count; i++) {
            
        }
        return totalPoint;
    }

    static void UpdatePoint() {
        
    }
}
// }

